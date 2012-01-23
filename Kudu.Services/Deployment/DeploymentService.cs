﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Json;
using System.ServiceModel;
using System.ServiceModel.Web;
using Kudu.Contracts;
using Kudu.Core.Deployment;

namespace Kudu.Services.Deployment
{
    [ServiceContract]
    public class DeploymentService
    {
        private readonly IDeploymentManager _deploymentManager;
        private readonly IProfiler _profiler;

        public DeploymentService(IProfiler profiler, IDeploymentManager deploymentManager)
        {
            _profiler = profiler;
            _deploymentManager = deploymentManager;
        }

        [Description("Gets the id of the current active deployment.")]
        [WebGet(UriTemplate = "id")]
        public string GetActiveDeploymentId()
        {
            using (_profiler.Step("DeploymentService.GetActiveDeploymentId"))
            {
                return _deploymentManager.ActiveDeploymentId;
            }
        }

        [Description("Deletes a deployment.")]
        [WebInvoke(UriTemplate = "delete")]
        public void Delete(JsonObject input)
        {
            using (_profiler.Step("DeploymentService.Delete"))
            {
                _deploymentManager.Delete((string)input["id"]);
            }
        }

        [Description("Performs a deployment.")]
        [WebInvoke(UriTemplate = "")]
        public void Deploy()
        {
            using (_profiler.Step("DeploymentService.Deploy"))
            {
                _deploymentManager.Deploy();
            }
        }

        [Description("Restore a previously successful deployment based on its id.")]
        [WebInvoke(UriTemplate = "restore")]
        public void Restore(JsonObject input)
        {
            using (_profiler.Step("DeploymentService.Restore"))
            {
                _deploymentManager.Deploy((string)input["id"]);
            }
        }

        [Description("Builds a specific deployment based on its id.")]
        [WebInvoke(UriTemplate = "build")]
        public void Build(JsonObject input)
        {
            using (_profiler.Step("DeploymentService.Build"))
            {
                _deploymentManager.Deploy((string)input["id"]);
            }
        }

        [Description("Gets the deployment results of all deployments.")]
        [WebGet(UriTemplate = "log")]
        public IEnumerable<DeployResult> GetDeployResults()
        {
            using (_profiler.Step("DeploymentService.GetDeployResults"))
            {
                return _deploymentManager.GetResults();
            }
        }

        [Description("Gets the log of a specific deployment based on its id.")]
        [WebGet(UriTemplate = "log?id={id}")]
        public IEnumerable<LogEntry> GetLogEntry(string id)
        {
            using (_profiler.Step("DeploymentService.GetLogEntry"))
            {
                return _deploymentManager.GetLogEntries(id);
            }
        }

        [Description("Gets the specified log entry details.")]
        [WebGet(UriTemplate = "logDetails?id={id}&entryId={entryId}")]
        public IEnumerable<LogEntry> GetLogEntryDetails(string id, string entryId)
        {
            using (_profiler.Step("DeploymentService.GetLogEntryDetails"))
            {
                return _deploymentManager.GetLogEntryDetails(id, entryId);
            }
        }

        [Description("Gets the deployment result of a specific deployment based on its id.")]
        [WebGet(UriTemplate = "details?id={id}")]
        public DeployResult GetResult(string id)
        {
            using (_profiler.Step("DeploymentService.GetResult"))
            {
                return _deploymentManager.GetResult(id);
            }
        }

        [Description("Gets the deployed files for a deployment based on its id.")]
        [WebGet(UriTemplate = "manifest?id={id}")]
        public IEnumerable<string> GetManifest(string id)
        {
            return _deploymentManager.GetManifest(id);
        }

    }
}
