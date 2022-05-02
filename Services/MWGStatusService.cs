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

        public bool AddMWGStatus(MWGStatusModel newMWGStatus)
        {
            bool result = false;
            bool doesMWGStatusExist = _context.MWGStatusInfo.SingleOrDefault(item => item.Id == newMWGStatus.Id) != null;
            if (!doesMWGExist)
            {
                _context.Add(newMWGStatus);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public IEnumerable<MWGStatusModel> GetAllMWGStatus()
        {
            return _context.MWGStatusInfo;
        }

        public MWGStatusModel GetMWGStatusById(int id)
        {
            return _context.MWGStatusInfo.Where(item => item.Id == id);
        }

        public MWGStatusModel GetMWGStatusByMWGId(int MWGId)
        {
            return _context.MWGStatusInfo.Where(item => item.MWGId == MWGId);
        }

        public MWGStatusModel GetMWGStatusByUserId(int UserId)
        {
            return _context.MWGStatusInfo.Where(item => item.UserId == UserId);
        }

        public bool UpdateGenreRanking(int MWGId, int UserId)
        {
            bool result = false;
            List <MWGStatusModel> allMWGStatusByMWGId = GetMWGStatusByMWGId(MWGId).ToList();
            MWGStatusModel allMWGStatusOfUser =  allMWGStatusByMWGId.Where(item => item.UserId == UserId);
            if (!allMWGStatusOfUser)
            {
                allMWGStatusOfUser.UserDoneWithGenreRankings = !allMWGStatusOfUser.UserDoneWithGenreRankings;
                _context.Update<MWGStatusModel>(allMWGStatusOfUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }
        public bool UpdateSwipings(int MWGId, int UserId)
        {
            bool result = false;
            List<MWGStatusModel> allMWGStatusByMWGId = GetMWGStatusByMWGId(MWGId).ToList();
            MWGStatusModel MWGwithUserId = allMWGStatusByMWGId.Where(item => item.UserId == UserId);
            if (!MWGwithUserId)
            {
                MWGwithUserId.UserDoneWithSwipes = !MWGwithUserId.UserDoneWithSwipes;
                _context.Update<MWGStatusModel>(MWGwithUserId);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }
        public bool UpdateSwipings(int MWGId)
        {
            bool result = false;
            List<MWGStatusModel> allMWGStatusByMWGId = GetMWGStatusByMWGId(MWGId).ToList();
            foreach(MWGStatusModel eachUser in allMWGStatusByMWGId)
            {
                eachUser.UserDoneWithSwipes = false;
                each.UserDoneWithGenreRankings = false;
                _context.Update<MWGStatusModel>(eachUser);
            }
            result = _context.SaveChanges() != 0;
            return result;
        }
    }
}