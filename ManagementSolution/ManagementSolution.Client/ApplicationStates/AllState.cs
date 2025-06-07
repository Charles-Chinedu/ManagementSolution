namespace ManagementSolution.Client.ApplicationStates
{
    public class AllState
    {
        // Scope action
        public Action? Action { get; set; }

        // General Department
        public bool ShowGeneralDepartment { get; set; }
        public void GeneralDepartmentClicked()
        {
            ResetAllDepartment();
            ShowGeneralDepartment = true;
            Action?.Invoke();
        }

        //Department
        public bool ShowDepartment { get; set; }

        public void DepartmentClicked()
        {
            ResetAllDepartment();
            ShowDepartment = true;
            Action?.Invoke();
        }
        // Branch
        public bool ShowBranch { get; set; }

        public void BranchClicked()
        {
            ResetAllDepartment();
            ShowBranch = true;
            Action?.Invoke();
        }

        // Country
        public bool ShowCountry { get; set; }

        public void CountryClicked()
        {
            ResetAllDepartment();
            ShowCountry = true;
            Action?.Invoke();
        }

        // City
        public bool ShowCity { get; set; }

        public void CityClicked()
        {
            ResetAllDepartment();
            ShowCity = true;
            Action?.Invoke();
        }

        public bool ShowTown { get; set; }

        public void TownClicked()
        {
            ResetAllDepartment();
            ShowTown = true;
            Action?.Invoke();
        }

        // User
        public bool ShowUser { get; set; }

        public void UserClicked()
        {
            ResetAllDepartment();
            ShowUser = true;
            Action?.Invoke();
        }

        // Doctor
        public bool ShowHealth { get; set; }

        public void HealthClicked()
        {
            ResetAllDepartment();
            ShowHealth = true;
            Action?.Invoke();
        }

        // Overtime
        public bool ShowOvertime { get; set; }

        public void OvertimeClicked()
        {
            ResetAllDepartment();
            ShowOvertime = true;
            Action?.Invoke();
        }

        // OvertimeType
        public bool ShowOvertimeType { get; set; }

        public void OvertimeTypeClicked()
        {
            ResetAllDepartment();
            ShowOvertimeType = true;
            Action?.Invoke();
        }

        // Sanction
        public bool ShowSanction { get; set; }

        public void SanctionClicked()
        {
            ResetAllDepartment();
            ShowSanction = true;
            Action?.Invoke();
        }

        // SanctionType
        public bool ShowSanctionType { get; set; }

        public void SanctionTypeClicked()
        {
            ResetAllDepartment();
            ShowSanctionType = true;
            Action?.Invoke();
        }

        // Vacation
        public bool ShowVacation { get; set; }

        public void VacationClicked()
        {
            ResetAllDepartment();
            ShowVacation = true;
            Action?.Invoke();
        }

        // VacationType
        public bool ShowVacationType { get; set; }

        public void VacationTypeClicked()
        {
            ResetAllDepartment();
            ShowVacationType = true;
            Action?.Invoke();
        }



        // Employee
        public bool ShowEmployee { get; set; } = true;

        public void EmployeeClicked()
        {
            ResetAllDepartment();
            ShowEmployee = true;
            Action?.Invoke();
        }

        // Show User Profile
        public bool ShowUserProfile { get; set; }

        public void UserProfileClicked()
        {
            ResetAllDepartment();
            ShowUserProfile = true;
            Action?.Invoke();
        }

        private void ResetAllDepartment()
        {
            ShowUserProfile = false;
            ShowGeneralDepartment = false;
            ShowDepartment = false;
            ShowBranch = false;
            ShowCountry = false;
            ShowCity = false;
            ShowTown = false;
            ShowUser = false;
            ShowEmployee = false;
            ShowHealth = false;
            ShowOvertime = false;
            ShowSanction = false;
            ShowVacation = false;
            ShowOvertimeType = false;
            ShowVacationType = false;
            ShowSanctionType = false;
        }
    }
}
