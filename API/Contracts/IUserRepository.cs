using API.Models;

namespace API.Contracts
{
    public interface IUserRepository : IGeneralRepository<User>
    {
        // Kelompok 1
        /*IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);*/

        // Kelompok 2
        /*int CreateWithValidate(Employee employee);*/

        // Kelompok 5
        Guid? FindGuidByEmail(string email);

        /*bool CheckEmailAndPhoneAndNIK(string value);*/

        /*public Employee GetByEmailAddress(string email);*/
    }
}
