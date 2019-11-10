using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesGrades.Services;
using RazorPagesGrades.ViewModels;

namespace RazorPagesGrades.Pages.GradeBook
{
    public class IndexModel : PageModel
    {
        ICertificate _certificate;
        IGradebook _gradebook;

        public IndexModel(ICertificate certificate, IGradebook gb)
        {
            _certificate = certificate;
            _gradebook = gb;
        }

        public IEnumerable<GradeViewModel> CertificateData { get; set; }

        public void OnGet()
        {
            foreach (var item in _gradebook.GetAllGrades())
            {
                _certificate.AddGrade(item);
            }

            CertificateData = _certificate.GetCertificateTable();
        }
    }
}