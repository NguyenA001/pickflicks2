using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pickflicks2.Models;
using pickflicks2.Services.Context;

namespace pickflicks2.Services
{
    public class InvitationService
    {
        private readonly DataContext _context;
        public InvitationService(DataContext context)
        {
            _context = context;
        }

        // -------- MWGservice functions ------------
        public bool AddMemberToMWG(int MWGId, int newMemberId, string? newMemberName)
        {
            bool result = false;
            MWGModel foundMWG = _context.MWGInfo.SingleOrDefault(item => item.Id == MWGId);
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
                    _context.SaveChanges();
                    //checks to see if MWGStatus for creator has been made
                    //if it hasn't then creates one for them
                    MWGStatusModel foundCreatorMWGStatus = _context.MWGStatusInfo.SingleOrDefault(mwg => mwg.UserId == foundMWG.GroupCreatorId && mwg.MWGId == foundMWG.Id);
                    if(foundCreatorMWGStatus == null)
                    {
                        MWGStatusModel newMWGStatusModelForGroupCreator = new MWGStatusModel();
                        newMWGStatusModelForGroupCreator.Id = 0;
                        newMWGStatusModelForGroupCreator.MWGId = MWGId;
                        newMWGStatusModelForGroupCreator.MWGName = foundMWG.MWGName;
                        newMWGStatusModelForGroupCreator.MembersId = foundMWG.MembersId;
                        newMWGStatusModelForGroupCreator.UserId = foundMWG.GroupCreatorId;
                        newMWGStatusModelForGroupCreator.MembersNames = foundMWG.MembersNames;
                        newMWGStatusModelForGroupCreator.GroupCreatorId = foundMWG.GroupCreatorId;
                        newMWGStatusModelForGroupCreator.UserDoneWithGenreRankings = false;
                        newMWGStatusModelForGroupCreator.UserDoneWithSwipes = false;
                        newMWGStatusModelForGroupCreator.IsDeleted = false;
                        newMWGStatusModelForGroupCreator.IsStarted = false;
                        _context.Add(newMWGStatusModelForGroupCreator);
                        result = _context.SaveChanges() !=0;
                    }
                    return result;
                }
            }
            return result;
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

        // -------InvitationService
        public bool AddInvitations( int MWGId, string? MWGName, string? stringOfInvitedUserNames)
        {
            // bool result = false;
            // InvitationModel doesInvitationExist = _context.InvitationInfo.SingleOrDefault(invite => invite.MWGId == newInvitation.MWGId && invite.UserId == newInvitation.UserId);
            // if (doesInvitationExist == null)
            // {
            //     _context.Add(newInvitation);
            //     result = _context.SaveChanges() != 0;
            // }
            // return result; 
            bool result = false;
            List<string> invitedUsers = new List<string>();
            //turn string of invited users into a list
            foreach(string user in stringOfInvitedUserNames.Split(","))
            {
                invitedUsers.Add(user);
            }
            //go thru list of string users and creates a new invitation model for each user
            foreach(string listUser in invitedUsers)
            {
                //checks to see if an invitation already exists for the user for this MWG
                InvitationModel doesInvitationExist = _context.InvitationInfo.SingleOrDefault(invite => invite.MWGId == MWGId && invite.UserName == listUser);
                //if no invitation exists..
                if (doesInvitationExist == null)
                {
                    //find the userModel matching the userName
                    UserModel foundUser = _context.UserInfo.SingleOrDefault(item => item.Username == listUser);
                    //create new instance of invitation model
                    InvitationModel newInvitationModel = new InvitationModel();
                    //fill in all required fields in model
                    newInvitationModel.Id = 0;
                    newInvitationModel.MWGId = MWGId;
                    newInvitationModel.MWGName = MWGName;
                    newInvitationModel.UserId = foundUser.Id;
                    newInvitationModel.UserName = listUser;
                    newInvitationModel.UserIcon = foundUser.Icon;
                    newInvitationModel.HasAccepted = false;
                    //add and save changes
                    _context.Add(newInvitationModel);
                    result = _context.SaveChanges() != 0;
                }
            }
            return result;
        }

        public IEnumerable<InvitationModel> GetAllInvitationsByMWGId(int MWGId)
        {
            return _context.InvitationInfo.Where(item => item.MWGId == MWGId);
        }

        public IEnumerable<InvitationModel> GetAllUnacceptedInvitationsByMWGId(int MWGId)
        {
            return _context.InvitationInfo.Where(item => item.MWGId == MWGId && item.HasAccepted == false);
        } 

        public IEnumerable<InvitationModel> GetAllAcceptedInvitationsByMWGId(int MWGId)
        {
            return _context.InvitationInfo.Where(item => item.MWGId == MWGId && item.HasAccepted == true);
        } 

        public IEnumerable<InvitationModel> GetAllInvitationsByUserId(int UserId)
        {
            return _context.InvitationInfo.Where(item => item.UserId == UserId);
        }  

        public IEnumerable<InvitationModel> GetAllUnacceptedInvitationsByUserId(int UserId)
        {
            return _context.InvitationInfo.Where(item => item.UserId == UserId && item.HasAccepted == false);
        } 

        public IEnumerable<InvitationModel> GetAllAcceptedInvitationsByUserId(int UserId)
        {
            return _context.InvitationInfo.Where(item => item.UserId == UserId && item.HasAccepted == true);
        } 

        public bool AcceptInvitation(int MWGId, int UserId)
        {
            bool result = false;
            var foundInvitation = GetAllUnacceptedInvitationsByUserId(UserId).SingleOrDefault(item => item.UserId == UserId && item.MWGId == MWGId);
            if(foundInvitation != null)
            {
                foundInvitation.HasAccepted = true;
                _context.Update<InvitationModel>(foundInvitation);
                _context.SaveChanges();
            }
            UserModel foundUser = _context.UserInfo.SingleOrDefault(item => item.Id == UserId);

            result = AddMemberToMWG(MWGId, UserId, foundUser.Username);
            return result;
        } 
    }
}