using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorPagesGrades.Models;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesGrades.Services
{
    public class GradebookDB : IGradebook, IGradeManipulator
    {
        readonly GradesContext _db;
        public GradebookDB(GradesContext db)
        {
            _db = db;
        }

        public bool AddGrade(string acronym, double value, int weight)
        {
            var grade = GradeCreate(acronym, value, weight);
            return AddGrade(grade);
        }

        public bool AddGrade(Grade grade)
        {
            if(_db.Grades.Where(g => g.Id == grade.Id).Count() > 0) return false;
            _db.Grades.Add(grade);
            _db.SaveChanges();
            return true;
        }

        public bool EditGrade(Grade grade)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Grade> GetAllGrades()
        {
            return _db.Grades.Include(g => g.Subject).ToList();
        }

        public Grade GetGrade(Guid id)
        {
            var grade = _db.Grades.SingleOrDefault(g => g.Id == id);
            _db.Entry<Grade>(grade).Reference(g => g.Subject).Load();
            return grade;
        }

        public IEnumerable<Grade> GetGrades(string subjectAcronym)
        {
            return _db.Grades.Include(g => g.Subject).Where(g => g.Subject.Acronym.Contains(subjectAcronym.ToUpper()));
        }

        public Grade GradeCreate(string acronym, double value, int weight)
        {
            var subject = _db.Subjects.SingleOrDefault(s => s.Acronym == acronym.ToUpper());
            if (subject is null) throw new KeyNotFoundException("Subject with Acronym " + acronym + " not found.");

            return new Grade() { Id = Guid.NewGuid(), Subject = subject, Value = value, Weight = weight };
        }

        public bool RemoveGrade(Guid id)
        {
            var item = _db.Grades.SingleOrDefault(g => g.Id == id);
            if (item is null) return false;

            _db.Grades.Remove(item);
            _db.SaveChanges();
            return true;
        }

        private readonly static Random random = new Random();

        public void SeedGrades(int count)
        {
            List<Subject> temp = _db.Subjects.ToList();
            if (temp.Count < 1) return;

            for (int i = 0; i < count; i++)
            {
                var subject = temp[random.Next(0, temp.Count)];
                var grade = new Grade() { Id = Guid.NewGuid(), Subject = subject, Value = random.Next(2, 11) * 0.5, Weight = random.Next(1, 11) };
                _db.Grades.Add(grade);
            }
            _db.SaveChanges();
        }
    }
}
