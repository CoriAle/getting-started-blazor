using BethanysPieShopHRM.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Pages
{
    public class EmployeeOverviewBase: ComponentBase
    {
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //Llmar al servicio
            Employees = (
                await EmployeeDataService.GetAllEmployees()
                ).ToList();
        }
        public IEnumerable<Employee> Employees { get; set; }

       
    }
}
