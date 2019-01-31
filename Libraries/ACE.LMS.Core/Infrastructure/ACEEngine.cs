﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Autofac;
using ACE.LMS.Core.Infrastructure.DependencyManagement;

namespace ACE.LMS.Core.Infrastructure
{
    public class ACEEngine : IEngine
    {
        #region Fields

        private ContainerManager _containerManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an instance of the content engine using default settings and configuration.
        /// </summary>
        public ACEEngine()
            : this(new ContainerConfigurer())
        {
        }

        public ACEEngine(ContainerConfigurer configurer)
        {
            //var config = ConfigurationManager.GetSection("INSOLConfig") as INSOLConfig;
            InitializeContainer(configurer);
        }

        #endregion

        #region Utilities

        //private void RunStartupTasks()
        //{
        //    var typeFinder = _containerManager.Resolve<ITypeFinder>();
        //    var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
        //    var startUpTasks = new List<IStartupTask>();
        //    foreach (var startUpTaskType in startUpTaskTypes)
        //        startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
        //    //sort
        //    startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
        //    foreach (var startUpTask in startUpTasks)
        //        startUpTask.Execute();
        //}

        private void InitializeContainer(ContainerConfigurer configurer)
        {
            var builder = new ContainerBuilder();

            _containerManager = new ContainerManager(builder.Build());
            configurer.Configure(this, _containerManager);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize components and plugins in the insol environment.
        /// </summary>
        /// <param name="config">Config</param>
        public void Initialize()
        {
            //bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();
            //if (databaseInstalled)
            //{
            //    //startup tasks
            //    RunStartupTasks();
            //}


            //startup tasks
            //if (!config.IgnoreStartupTasks)
            //{
            //    RunStartupTasks();
            //}
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        #endregion

        #region Properties

        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        #endregion
    }
}
