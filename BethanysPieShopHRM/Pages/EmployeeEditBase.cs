using BethanysPieShopHRM.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Pages
{
    public class EmployeeEditBase : ComponentBase
    {
        //Injectar el servicio
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        
        [Inject]
        public ICountryDataService CountryDataService { get; set; }
        
        [Inject]
        public IJobCategoryDataService JobCategoryDataService { get; set; }

        //Parámetro
        [Parameter]
        public string EmployeeId { get; set; }

        
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();


        //Instancia del objeto
        public Employee Employee { get; set; } = new Employee();
        
        //Blazor aún no soporta data-binding con ints, solo strings
        public string CountryId { get;  set; }
        protected string JobCategoryId = string.Empty;


        public bool Saved { get;  set; }
        public string StatusClass { get;  set; }
        public string Message { get;  set; }


        //Navegcion
        [Inject]
        public NavigationManager NavigationManager { get; set; }



        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            Countries = (await CountryDataService.GetAllCountries()).ToList();
            JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();

            int.TryParse(EmployeeId, out var employeeId);

            if (employeeId == 0) //new employee is being created
            {
                //add some defaults
                Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
            }
            else
            {
               // Countries = (await CountryDataService.GetAllCountries()).ToList();
                //JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();
                Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
                
                //CountryId = Employee.CountryId.ToString();
                //JobCategoryId = Employee.JobCategoryId.ToString();


             }

             CountryId = Employee.CountryId.ToString();
             JobCategoryId = Employee.JobCategoryId.ToString();
        }
        protected async Task HandleValidSubmit()
        {
            Saved = false;
            //Iguala a las propiedades del objeto
            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);

            if (Employee.EmployeeId == 0) //new
            {
                var addedEmployee = await EmployeeDataService.AddEmployee(Employee);
                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "Nuevo empleado agregado con éxito";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Huo un error al agregar un empleado. Por favor intente de nuevo más tarde.";
                    Saved = false;
                }
            }
            else
            {
                await EmployeeDataService.UpdateEmployee(Employee);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
                Saved = true;
            }
        }

        protected async Task DeleteEmployee()
        {
            await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToOverview()
        {
            //Ir a overwiev
            NavigationManager.NavigateTo("/employeeoverview");

        }

        //
    }


}
