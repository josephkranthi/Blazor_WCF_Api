using WebApi.Models;
using System.ServiceModel;

namespace WebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly string _wcfServiceUrl;

        public EmployeeService(ILogger<EmployeeService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _wcfServiceUrl = configuration["WcfService:EmployeeServiceUrl"] ?? "http://localhost:54640/EmployeeService.svc";
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            _logger.LogInformation("GetEmployeesAsync called");
            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(_wcfServiceUrl);

                var channelFactory = new ChannelFactory<IEmployeeServiceChannel>(binding, endpoint);
                var channel = channelFactory.CreateChannel();

                try
                {
                    // WCF service methods are synchronous, wrap in Task
                    // synchronous - simplest
                    // if you need an async call
                    var result = await Task.Run(() => channel.GetEmployees().ToList());
                    _logger.LogInformation("Retrieved {Count} employees", result?.Count ?? 0);
                    return result?.ToList() ?? new List<Employee>();
                }
                finally
                {
                    ((IDisposable)channel)?.Dispose();
                    channelFactory?.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling WCF GetEmployees");
                throw;
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            _logger.LogInformation("GetEmployeeByIdAsync called for Id {Id}", id);
            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(_wcfServiceUrl);

                var channelFactory = new ChannelFactory<IEmployeeServiceChannel>(binding, endpoint);
                var channel = channelFactory.CreateChannel();

                try
                {
                    var result = await Task.FromResult(channel.GetEmployeeById(id));
                    if (result == null)
                    {
                        _logger.LogWarning("Employee not found with Id {Id}", id);
                    }
                    return result;
                }
                finally
                {
                    ((IDisposable)channel)?.Dispose();
                    channelFactory?.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling WCF GetEmployeeById for Id {Id}", id);
                throw;
            }
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            _logger.LogInformation("AddEmployeeAsync called for Employee {Name}", employee?.Name);
            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(_wcfServiceUrl);

                var channelFactory = new ChannelFactory<IEmployeeServiceChannel>(binding, endpoint);
                var channel = channelFactory.CreateChannel();

                try
                {
                    var result = await Task.FromResult(channel.AddEmployee(employee));
                    _logger.LogInformation("Employee added with Id {Id}", result);
                    return result;
                }
                finally
                {
                    ((IDisposable)channel)?.Dispose();
                    channelFactory?.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling WCF AddEmployee");
                throw;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            _logger.LogInformation("UpdateEmployeeAsync called for Employee Id {Id}", employee?.Id);
            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(_wcfServiceUrl);

                var channelFactory = new ChannelFactory<IEmployeeServiceChannel>(binding, endpoint);
                var channel = channelFactory.CreateChannel();

                try
                {
                    var result = await Task.FromResult(channel.UpdateEmployee(employee));
                    _logger.LogInformation("Employee {Id} update result: {Success}", employee?.Id, result);
                    return result;
                }
                finally
                {
                    ((IDisposable)channel)?.Dispose();
                    channelFactory?.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling WCF UpdateEmployee for Id {Id}", employee?.Id);
                throw;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            _logger.LogInformation("DeleteEmployeeAsync called for Id {Id}", id);
            try
            {
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(_wcfServiceUrl);

                var channelFactory = new ChannelFactory<IEmployeeServiceChannel>(binding, endpoint);
                var channel = channelFactory.CreateChannel();

                try
                {
                    var result = await Task.FromResult(channel.DeleteEmployee(id));
                    _logger.LogInformation("Employee {Id} delete result: {Success}", id, result);
                    return result;
                }
                finally
                {
                    ((IDisposable)channel)?.Dispose();
                    channelFactory?.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling WCF DeleteEmployee for Id {Id}", id);
                throw;
            }
        }
    }

    // WCF Service Channel Interface - matches the WcfService1.IEmployeeService contract
    [ServiceContract(Name = "IEmployeeService", Namespace = "http://tempuri.org/")]
    public interface IEmployeeServiceChannel
    {
        [OperationContract]
        List<Employee> GetEmployees();

        [OperationContract]
        Employee GetEmployeeById(int id);

        [OperationContract]
        int AddEmployee(Employee employee);

        [OperationContract]
        bool UpdateEmployee(Employee employee);

        [OperationContract]
        bool DeleteEmployee(int id);
    }
}
