using BethanysPieShopHRM.Components;
using BethanysPieShopHRM.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Pages
{
    public class EmployeeOverviewBase: ComponentBase
    {
        //Para acceder al usuario en el componente.
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        protected AddEmployeeDialog AddEmployeeDialog { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //Llmar al servicio
            Employees = (
                await EmployeeDataService.GetAllEmployees()
                ).ToList();
        }


        protected async Task QuickAddEmployee()
        {
            var authenticationState = await authenticationStateTask;
            if (authenticationState.User.Identity.Name == "Cori")
            {
                AddEmployeeDialog.Show();
            }
        }
        public async void AddEmployeeDialog_OnDialogClose()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            StateHasChanged();
        }



    }
}
