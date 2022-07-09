using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.Models
{
    public class CSelectList
    {
        public static SelectList ToSelectList(List<Category> lstCatrgory)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (Category item in lstCatrgory)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.CategoriesName,
                    Value = Convert.ToString(item.CategoryId)
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        public static SelectList ToSelectList(List<Country> lstCountry)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (Country item in lstCountry)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.CountryName,
                    Value = Convert.ToString(item.CountryId)
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        public static SelectList ToSelectList(List<Package> lstPackage)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (Package item in lstPackage)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.PackageName,
                    Value = Convert.ToString(item.PackageId)
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        public static SelectList ToSelectList(List<Process> lstProcess)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (Process item in lstProcess)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.ProcessName,
                    Value = Convert.ToString(item.ProcessId)
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        public static SelectList ToSelectList(List<Roasting> lstRoasting)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (Roasting item in lstRoasting)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.RoastingName,
                    Value = Convert.ToString(item.RoastingId)
                });
            }

            return new SelectList(list, "Value", "Text");
        }
    }
}
