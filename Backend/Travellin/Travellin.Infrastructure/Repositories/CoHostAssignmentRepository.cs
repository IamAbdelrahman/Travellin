//using Travellin.Travellin.Core.Entities;
//using Travellin.Travellin.Core.Interfaces;
//using Travellin.Travellin.Infrastructure.Data;

//namespace Travellin.Travellin.Infrastructure.Repositories
//{
//    public class CoHostAssignmentRepository : ICoHostAssignmentRepository
//    {
//        private readonly AirbnbDbContext db;

//        public CoHostAssignmentRepository(AirbnbDbContext db)
//        {
//            this.db = db;
//        }

//        //GetALL
//        public List<CoHostAssignment> GetAll()
//        {
//            return db.co
//        }

//        //GetByID
//        public CoHostAssignment GetByID(int id)
//        {
//            return
//        }

//        //Add
//        public void Add(CoHostAssignment entity)
//        {
//            db.Add(entity);
//        }

//        //Delete
//        public void Delete(int id)
//        {

//        }

//        //Update
//        public void Update(CoHostAssignment entity)
//        {
//            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
//        }

//        //Save
//        public void Save()
//        {
//            db.SaveChanges();
//        }
//    }
//}
