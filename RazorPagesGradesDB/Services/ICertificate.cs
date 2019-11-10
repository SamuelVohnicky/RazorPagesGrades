using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorPagesGrades.Models;
using RazorPagesGrades.ViewModels;

namespace RazorPagesGrades.Services
{
    public interface ICertificate
    {
        bool AddGrade(Grade grade);
        IEnumerable<GradeViewModel> GetCertificateTable();
    }
}
