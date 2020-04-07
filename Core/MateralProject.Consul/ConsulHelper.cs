using Consul;
using MateralProject.Core;
using System;
using System.Threading.Tasks;

namespace MateralProject.Consul
{
    public class ConsulHelper
    {
        private readonly ConsulConfigModel _consulConfig;
        private readonly ServiceConfigModel _serviceConfig;
        private ConsulClient consulClient;
        private string consulID;
        public event Action<string> OnMessage;
        public event Action<Exception> OnException;
        public ConsulHelper(ConsulConfigModel consulConfig, ServiceConfigModel serviceConfig)
        {
            _consulConfig = consulConfig;
            _serviceConfig = serviceConfig;
        }
        /// <summary>
        /// 注册Consul
        /// </summary>
        /// <returns></returns>
        public async Task RegisterConsulAsync()
        {
            if (!_consulConfig.Enable) return;
            OnMessage?.Invoke("开始注册Consul");
            try
            {
                consulClient = new ConsulClient(options => options.Address = new Uri(_consulConfig.ConsulAddress));
                consulID = $"{_serviceConfig.ServiceName}_{_serviceConfig.ServiceUrl}";
                var agentServiceRegistration = new AgentServiceRegistration
                {
                    ID = consulID,
                    Name = _serviceConfig.ServiceName,
                    Address = _serviceConfig.ServiceAddress,
                    Port = _serviceConfig.ServicePort,
                    Tags = new string[] { },
                    Checks = new[]
                    {
                        new AgentServiceCheck
                        {
                            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                            HTTP = _serviceConfig.ServiceHealthUrl,
                            Interval = TimeSpan.FromSeconds(10),
                            Timeout = TimeSpan.FromSeconds(5),
                        }
                    },
                };
                await consulClient.Agent.ServiceRegister(agentServiceRegistration);
                OnMessage?.Invoke("Consul注册完毕");
            }
            catch (Exception exception)
            {
                exception = new MateralProjectException("Consul注册失败", exception);
                OnException?.Invoke(exception);
            }
        }
        /// <summary>
        /// 反注册Consul
        /// </summary>
        /// <returns></returns>
        public void UnRegisterConsul()
        {
            if (!_consulConfig.Enable) return;
            OnMessage?.Invoke("开始反注册Consul");
            try
            {
                consulClient.Dispose();
                consulClient = null;
                consulID = null;
                OnMessage?.Invoke("Consul反注册完毕");
            }
            catch (Exception exception)
            {
                exception = new MateralProjectException("Consul反注册失败", exception);
                OnException?.Invoke(exception);
            }
        }
    }
}
