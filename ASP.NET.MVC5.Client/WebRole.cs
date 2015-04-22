
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.Diagnostics.Management;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace ASP.NET.MVC5.Client
{
    public class WebRole : RoleEntryPoint
    {
        public const string WADConnectionString = "Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString";
        public const string WADPerformanceCountersTable = "WADPerformanceCountersTable";

        /// <summary>
        /// The string used to separate performance counter specifiers in the
        /// WebRolePerfCounterConfig entry in the service configuration file.
        /// </summary>
        public const string ConfigSeparator = ";;";

        /// <summary>
        /// The entries in the configuration file for performance counters.
        /// </summary>
        public const string WebRoleConfigName = "WebRolePerfCounterConfig";
        public const string WebRolePeriodName = "WebRolePerfCounterTransferPeriod";
        public const string WebRoleSampleRateName = "WebRolePerfCounterSampleRate";




        /// <summary>
        /// Override the OnStart handler to add our event handlers for RoleEnvironment changes and
        /// to initialize the PerformanceCounters as part of the Diagnostic Monitor configuration.
        /// </summary>
        /// <returns>Returns true if initialization succeeds, otherwise false.</returns>
        public override bool OnStart()
        {

            RoleEnvironment.Changing += this.RoleEnvironmentChanging;
            RoleEnvironment.Changed += this.RoleEnvironmentChanged;
            return base.OnStart();
        }


        /// <summary>
        /// Array of ConfigurationSettings setting names that will not force the role to be recycled when changed.
        /// </summary>
        private static string[] nonRecycleConfigurationItems = new[]
            {
                WebRoleConfigName, WebRolePeriodName, WebRoleSampleRateName
            };




        /// <summary>
        /// Check service configuration update for changes that we cannot handle without a recycle.
        /// </summary>
        /// <param name="changes">Collection of changes from RoleEnvironmentChanging event.</param>
        /// <returns>True if a non-handled configuration change was made, requiring a role recycle.</returns>
        private bool RecycleConfiguration(ReadOnlyCollection<RoleEnvironmentChange> changes)
        {
            Func<RoleEnvironmentConfigurationSettingChange, bool> changeForcesRecycle =
                    x => !nonRecycleConfigurationItems.Contains(x.ConfigurationSettingName);

            return changes.OfType<RoleEnvironmentConfigurationSettingChange>().Any(changeForcesRecycle);
        }

        /// <summary>
        /// Event handler called when an environment change is to be applied to the role.
        /// Determines whether or not the role instance must be recycled.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The list of changed environment values.</param>
        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            // If Azure should recycle the role, e.Cancel should be set to true.
            // If the changes are ones we can handle without a recycle, we set it to false.
            e.Cancel = this.RecycleConfiguration(e.Changes);
            Trace.WriteLine("WebRole environment change - role instance recycling: " + e.Cancel.ToString());
        }

        /// <summary>
        /// Event handler called after an environment change has been applied to the role.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The list of changed environment values.</param>
        private void RoleEnvironmentChanged(object sender, RoleEnvironmentChangedEventArgs e)
        {

        }
    }
}