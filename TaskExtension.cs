/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Sage.WebAuthenticationBroker
{
    /// <summary>
    /// Task extension class.
    /// </summary>
    public static class TaskExtension
    {
        /// <summary>
        /// Waits on the task and provides message processing. 
        /// </summary>
        /// <param name="task">The task to wait on.</param>
        public static void WaitWithPumping(this Task task)
        {
            if (task == null) throw new ArgumentNullException("task");

            var nestedFrame = new DispatcherFrame();

            task.ContinueWith(t => nestedFrame.Continue = false);

            Dispatcher.PushFrame(nestedFrame);
        }
    }
}
