using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pickflicks2.Models;
using pickflicks2.Models.DTO;
using pickflicks2.Services;
using pickflicks2.Services.Context;

namespace pickflicks2.Services
{
    public class MWGService
    {
        private readonly DataContext _context;
        public MWGService(DataContext context)
        {
            _context = context;
        }

        public bool DeleteMWGStatus(int MWGId, int userId)
        {
            bool result = false;
            MWGStatusModel foundUser =  _context.MWGStatusInfo.SingleOrDefault(item => item.MWGId == MWGId && item.UserId == userId);
            _context.Remove<MWGStatusModel>(foundUser);
            result = _context.SaveChanges() != 0;
            return result;
        }

        public bool AllUsersDoneGenre(int MWGId)
        {
             List<MWGStatusModel> allMWGStatusModelOfMWG =  _context.MWGStatusInfo.Where(item => item.MWGId == MWGId).ToList();

             //loop thru list and check to see if all user.donewithgenre is true

            List<MWGStatusModel> allCompleted = allMWGStatusModelOfMWG.Where(user => user.UserDoneWithGenreRankings == false).ToList();

            //if anyone in MWG is not done, will return false
            if(allCompleted.Count > 0)
            {
                return false;
            }else{
                return true;
            }
        }

        public bool AllUsersDoneSwipes(int MWGId)
        {
            List<MWGStatusModel> allMWGStatusModelOfMWG =  _context.MWGStatusInfo.Where(item => item.MWGId == MWGId).ToList();

            List<MWGStatusModel> allCompleted = allMWGStatusModelOfMWG.Where(user => user.UserDoneWithSwipes == false).ToList();

            //if anyone in MWG is not done, will return false
            if(allCompleted.Count > 0)
            {
                return false;
            }else{
                return true;
            }
        }
        public IEnumerable<MWGStatusModel> GetMWGStatusByMWGId(int MWGId)
        {
            bool areAllDoneGenre = AllUsersDoneGenre(MWGId);
            bool areAllDoneSwipe = AllUsersDoneSwipes(MWGId);

            List<MWGStatusModel> allMWGStatusModels = _context.MWGStatusInfo.Where(item => item.MWGId == MWGId).ToList();
            foreach(MWGStatusModel mwg in allMWGStatusModels)
            {
                mwg.AreAllMembersDoneWithGenre = areAllDoneGenre;
                mwg.AreAllMembersDoneWithSwipes = areAllDoneSwipe;
            }
            return allMWGStatusModels;
        }
        public bool UpdateMWGStatus(int MWGId)
        {
            bool result = false;
            MWGModel foundMWG = _context.MWGInfo.SingleOrDefault(item => item.Id == MWGId);
            List <MWGStatusModel> allMWGStatusOfMWGID = GetMWGStatusByMWGId(MWGId).ToList();
            foreach(MWGStatusModel statusModel in allMWGStatusOfMWGID)
            {
                statusModel.MembersId = foundMWG.MembersId;
                statusModel.MembersNames = foundMWG.MembersNames;
                statusModel.MembersId = foundMWG.MembersId;
                statusModel.MWGName = foundMWG.MWGName;
                statusModel.IsDeleted = foundMWG.IsDeleted;

                _context.Update<MWGStatusModel>(statusModel);
            }
            result = _context.SaveChanges() != 0;
            return result;
        }

        public bool AddMWG(MWGModel newMWGModel)
        {
            bool result = false;
            bool doesMWGExist = _context.MWGInfo.SingleOrDefault(MWG => MWG.MWGName == newMWGModel.MWGName) != null;
            if (!doesMWGExist)
            {
                _context.Add(newMWGModel);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public IEnumerable<MWGModel> GetAllMWG()
        {
            return _context.MWGInfo;
        }

        public MWGModel GetMWGById(int id)
        {
            return _context.MWGInfo.SingleOrDefault(item => item.Id == id);
        }

        public MWGModel GetMWGByMWGName(string MWGName)
        {
            return _context.MWGInfo.SingleOrDefault(item => item.MWGName == MWGName);
        }

        public IEnumerable<MWGModel> GetAllCreatedMWGByUserId(int userId)
        {
            return _context.MWGInfo.Where(item => item.GroupCreatorId == userId);
        }

        public List<MWGModel> GetAllMWGAUserIsMemberOf(int userId)
        {
            //"Tag1, Tag2, Tag3,Tag4"
            List<MWGModel> AllMWGWithMemberId = new List<MWGModel>();//[]
            var allMWG = GetAllMWG().ToList();//{Tag:"Tag1, Tag2",Tag:"Tag2",Tag:"tag3"}
            for (int i = 0; i < allMWG.Count; i++)
            {
                MWGModel Group = allMWG[i];//{Tag:"Tag1, Tag2"}
                var groupArr = Group.MembersId.Split(",");//["Tag1","Tag2"]
                for (int j = 0; j < groupArr.Length; j++)
                {   //Tag1 j = 0
                    //Tag2 j = 1
                    if (groupArr[j].Contains(userId.ToString()))
                    {// Tag1               Tag1
                        AllMWGWithMemberId.Add(Group);//{Tag:"Tag1, Tag2"}
                    }
                }
            }
            return AllMWGWithMemberId;
        }

        public bool EditMWGName(string? oldMWGName, string? updatedMWGName)
        {
            //bool result = false;
            MWGModel foundMWG = GetMWGByMWGName(oldMWGName);
            if (foundMWG != null)
            {
                foundMWG.MWGName = updatedMWGName;
                _context.Update<MWGModel>(foundMWG);
                //result = _context.SaveChanges() != 0;
            }

            bool didItWork = UpdateMWGStatus(foundMWG.Id);

            return didItWork;
        }

        // when you add a member, you should send them an invite first ant they need to accept it
        
        public bool AddMemberToMWG(int MWGId, int newMemberId, string? newMemberName)
        {
            bool result = false;
            MWGModel foundMWG = GetMWGById(MWGId);
            UserModel foundUser = _context.UserInfo.SingleOrDefault(item => item.Id == newMemberId);

            if (foundMWG != null)
            {
                // Append the new userId into the string
                foundMWG.MembersId += ',' + newMemberId.ToString();
                foundMWG.MembersNames += ',' + newMemberName;
                foundMWG.MembersIcons += ',' + foundUser.Icon;
                _context.Update<MWGModel>(foundMWG);
                result = _context.SaveChanges() != 0;
                if(result)
                {
                    //add new member to MWGstatus models too
                    UpdateMWGStatus(MWGId);
                    MWGStatusModel newMWGStatusModel = new MWGStatusModel();
                    newMWGStatusModel.Id = 0;
                    newMWGStatusModel.MWGId = MWGId;
                    newMWGStatusModel.MWGName = foundMWG.MWGName;
                    newMWGStatusModel.MembersId = foundMWG.MembersId;
                    newMWGStatusModel.UserId = newMemberId;
                    newMWGStatusModel.MembersNames = foundMWG.MembersNames;
                    newMWGStatusModel.GroupCreatorId = foundMWG.GroupCreatorId;
                    newMWGStatusModel.UserDoneWithGenreRankings = false;
                    newMWGStatusModel.UserDoneWithSwipes = false;
                    newMWGStatusModel.IsDeleted = false;
                    newMWGStatusModel.IsStarted = false;
                    
                    _context.Add(newMWGStatusModel);
                    result = _context.SaveChanges() != 0;
                    return result;
                }
            }
            return result;
        }

        public bool AddUserSuggestedMovies(int MWGId, string? newMovie)
        {
            bool result = false;
            MWGModel foundMWG = GetMWGById(MWGId);
            if (foundMWG != null)
            {
                // Append the new userId into the string
                foundMWG.UserSuggestedMovies += ',' + newMovie;
                _context.Update<MWGModel>(foundMWG);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }


        public bool DeleteMemberFromMWG(int MWGId, int deletedMemberId, string? deleteMemberName)
        {
            bool result = false;
            MWGModel foundMWG = GetMWGById(MWGId);
            UserModel foundUser = _context.UserInfo.SingleOrDefault(item => item.Id == deletedMemberId);

            if (foundMWG != null)
            {
                int position = foundMWG.MembersId.IndexOf(deletedMemberId.ToString());
                int namePosition = foundMWG.MembersNames.IndexOf(deleteMemberName);
                int iconPosition = foundMWG.MembersIcons.IndexOf(foundUser.Icon);

                int lengthOfDeletedMemberId = deletedMemberId.ToString().Length;
                int lengthOfDeletedMemberName = deleteMemberName.Length;
                int lengthOfDeletedMemberIcon = foundUser.Icon.Length;

                if (position == foundMWG.MembersId.Length - lengthOfDeletedMemberId)
                {
                    foundMWG.MembersId = foundMWG.MembersId.Remove(position - 1, lengthOfDeletedMemberId+1);
                    foundMWG.MembersNames = foundMWG.MembersNames.Remove(namePosition - 1, lengthOfDeletedMemberName+1);
                    foundMWG.MembersIcons = foundMWG.MembersIcons.Remove(iconPosition -1, lengthOfDeletedMemberIcon+1);
                }
                else
                {
                    foundMWG.MembersId = foundMWG.MembersId.Remove(position, lengthOfDeletedMemberId+1);
                    foundMWG.MembersNames = foundMWG.MembersNames.Remove(namePosition, lengthOfDeletedMemberName+1);
                    foundMWG.MembersIcons = foundMWG.MembersIcons.Remove(iconPosition, lengthOfDeletedMemberIcon+1);
                }
                _context.Update<MWGModel>(foundMWG);
                result = _context.SaveChanges() != 0;
                if(result)
                {
                    //update MWG model to remove deleted member
                    bool didItWork = UpdateMWGStatus(MWGId);
                    //delete MWGStatus model of deleted member
                    bool didItDelete = DeleteMWGStatus(MWGId, deletedMemberId);
                    //delete the invitation model of the member (so they can be invited again)

                    return didItDelete;
                }
            }
            return result;

        }

        public bool DeleteByMWGName(string? MWGName)
        {
            bool result = false;
            MWGModel foundMWG = GetMWGByMWGName(MWGName);
            if (foundMWG != null)
            {
                foundMWG.IsDeleted = !foundMWG.IsDeleted;
                _context.Update<MWGModel>(foundMWG);
                result = _context.SaveChanges() != 0;
                if(result)
                {
                    bool didItWork = UpdateMWGStatus(foundMWG.Id);
                return didItWork;
                }
            }
            return result;
        }

        public bool DeleteByMWGId(int MWGId)
        {
            bool result = false;
            MWGModel foundMWG = GetMWGById(MWGId);
            if (foundMWG != null)
            {
                foundMWG.IsDeleted = !foundMWG.IsDeleted;
                _context.Update<MWGModel>(foundMWG);
                result = _context.SaveChanges() != 0;
                if(result)
                {
                    bool didItWork = UpdateMWGStatus(foundMWG.Id);
                    return didItWork;
                }
            }
            return result;
        }


        //use this for backend
        public bool EditMWG(MWGModel MWG)
        {
            _context.Update<MWGModel>(MWG);
            return _context.SaveChanges() != 0;
        }

        public bool AddChosenGenres(int MWGId, string chosenGenres)
        {
            bool result = false;
            MWGModel foundMWG = GetMWGById(MWGId);
            if (foundMWG != null)
            {
                // Append the new userId into the string
                foundMWG.ChosenGenres = chosenGenres;
                _context.Update<MWGModel>(foundMWG);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public bool AddStreamingService(int MWGId, string serviceId)
        {
         bool result = false;
            MWGModel foundMWG = GetMWGById(MWGId);
            if (foundMWG != null)
            {
                // Append the new userId into the string
                foundMWG.StreamingService = serviceId;
                _context.Update<MWGModel>(foundMWG);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

         public bool AddFinalGenre(int MWGId, string GenreName)
        {
            bool result = false;
            MWGModel foundMWG = GetMWGById(MWGId);
            if (foundMWG != null)
            {
                foundMWG.FinalGenre = GenreName;
                _context.Update<MWGModel>(foundMWG);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }
         public bool AddFinalMovieIndex(int MWGId, int index)
        {
            bool result = false;
            MWGModel foundMWG = GetMWGById(MWGId);
            if (foundMWG != null)
            {
                // Append the new userId into the string
                foundMWG.FinalMovieIndex = index;
                _context.Update<MWGModel>(foundMWG);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }
    }
}