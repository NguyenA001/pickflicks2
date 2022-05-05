using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pickflicks2.Models;
using pickflicks2.Services.Context;

namespace pickflicks2.Services
{
    public class MWGStatusService
    {
        private readonly DataContext _context;

        public MWGStatusService(DataContext context)
        {
            _context = context;
        }

        public bool AddMWGStatus(int MWGId)
        {
            bool result = false;
            MWGModel foundMWG =  _context.MWGInfo.SingleOrDefault(item => item.Id == MWGId);
            string foundMWGmembersId = foundMWG.MembersId;
            string foundMWGmembersNames = foundMWG.MembersNames;
            int foundMWGCreatorId = foundMWG.GroupCreatorId;

            List<int> MWGmembersIdlist = new List<int>();
            foreach (string memberId in foundMWGmembersId.Split(','))
            MWGmembersIdlist.Add(Int32.Parse(memberId));

            foreach(int userId in MWGmembersIdlist)
            {
                MWGStatusModel newMWGStatusModel = new MWGStatusModel();
                newMWGStatusModel.Id = 0;
                newMWGStatusModel.MWGId = MWGId;
                newMWGStatusModel.MembersId = foundMWGmembersId;
                newMWGStatusModel.UserId = userId;
                newMWGStatusModel.MembersNames = foundMWGmembersNames;
                newMWGStatusModel.GroupCreatorId = foundMWGCreatorId;
                newMWGStatusModel.UserDoneWithGenreRankings = false;
                newMWGStatusModel.UserDoneWithSwipes = false;
                newMWGStatusModel.IsDeleted = false;
                newMWGStatusModel.IsStarted = false;
                
                 _context.Add(newMWGStatusModel);
                result = _context.SaveChanges() != 0;
                
            }
            return result;



            return result;
        }

        public IEnumerable<MWGStatusModel> GetAllMWGStatus()
        {
            return _context.MWGStatusInfo;
        }

        public MWGStatusModel GetMWGStatusById(int id)
        {
            return _context.MWGStatusInfo.SingleOrDefault(item => item.Id == id);
        }

        public IEnumerable<MWGStatusModel> GetMWGStatusByMWGId(int MWGId)
        {
            return _context.MWGStatusInfo.Where(item => item.MWGId == MWGId);
        }

        public IEnumerable<MWGStatusModel> GetMWGStatusByUserId(int UserId)
        {
            return _context.MWGStatusInfo.Where(item => item.UserId == UserId);
        }

        public bool UpdateGenreRanking(int MWGId, int UserId)
        {
            bool result = false;
            var foundMWGStatus = GetAllMWGStatus().SingleOrDefault(item => item.UserId == UserId && item.MWGId == MWGId);
            if(foundMWGStatus != null)
            {
            foundMWGStatus.UserDoneWithGenreRankings = !foundMWGStatus.UserDoneWithGenreRankings;
            _context.Update<MWGStatusModel>(foundMWGStatus);
            result = _context.SaveChanges() != 0;
            }
            return result;
        }
        public bool UpdateSwipings(int MWGId, int UserId)
        {
            bool result = false;
            var foundMWGStatus = GetAllMWGStatus().SingleOrDefault(item => item.UserId == UserId && item.MWGId == MWGId);
            if(foundMWGStatus != null)
            {
            foundMWGStatus.UserDoneWithSwipes = !foundMWGStatus.UserDoneWithSwipes;
            _context.Update<MWGStatusModel>(foundMWGStatus);
            result = _context.SaveChanges() != 0;
            }
            return result;
        }
        public bool ResetMWGStatusbyMWGId(int MWGId)
        {
            bool result = false;
            List<MWGStatusModel> allMWGStatusByMWGId = GetMWGStatusByMWGId(MWGId).ToList();
            foreach(MWGStatusModel eachUser in allMWGStatusByMWGId)
            {
                eachUser.UserDoneWithSwipes = false;
                eachUser.UserDoneWithGenreRankings = false;
                _context.Update<MWGStatusModel>(eachUser);
            }
            result = _context.SaveChanges() != 0;
            return result;
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
    }
}