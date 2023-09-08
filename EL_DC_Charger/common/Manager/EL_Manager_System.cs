using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace EL_DC_Charger.Manager
{
    public class EL_Mananger_System
    {
        public static void startService_TimeSynchronization()
        {
            try
            {
                foreach (ServiceController service in ServiceController.GetServices())
                    if (service.ServiceName == "W32Time" && service.Status == ServiceControllerStatus.Stopped)
                    {
                        //logger.Info("Service - W32Time is not started... starting...");
                        service.Start();
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";
                        startInfo.Arguments = "/C sc.exe config W32Time start=auto";
                        process.StartInfo = startInfo;
                        process.Start();
                    }
            }
            catch (Exception ex)
            {
                //logger.Info("Service search and started error... " + ex);
            }
        }

        public static void startService_Plz_AdminMode()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity is null)
            {
                MessageBox.Show("관리자 권한 실행 필요.");
                Environment.Exit(1);
            }
            else
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    MessageBox.Show("관리자 권한 실행 필요.");
                    Environment.Exit(1);
                }
            }
        }

        public static void finishApplication_By_NotSuppotDevice(string userName, string[] device)
        {
            string releaseId = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "").ToString();

            bool isNeedFinish = false;

            if (userName != null && userName.Length > 0)
            {
                if (Environment.UserName != userName)
                    isNeedFinish = true;

            }

            if (device != null && device.Length > 0)
            {
                bool checkEqualsDevice = false;
                for (int i = 0; i < device.Length; i++)
                {
                    if (device[i] == releaseId)
                        checkEqualsDevice = true;
                }
                if (!checkEqualsDevice)
                    isNeedFinish = true;
            }



            if (isNeedFinish)
            {
                MessageBox.Show("프로그램 지원 장비에 해당되지 않습니다.\n단말기를 확인 해 주세요.");
                Environment.Exit(1);
            }
        }



        static void EnumFolderTasks(TaskFolder fld)
        {
            foreach (Task task in fld.Tasks)
                ActOnTask(task);
            foreach (TaskFolder sfld in fld.SubFolders)
                EnumFolderTasks(sfld);
        }

        static void ActOnTask(Task t)
        {
            mTasks_name.Add(t.Name);
        }

        public static void taskManager_UpdateTaskList()
        {
            using (TaskService ts = new TaskService())
            {
                EnumFolderTasks(ts.RootFolder);
                mTasks_name.Sort();
            }

        }

        public static void taskManager_DisplayTaskList()
        {
            mTasks_name.Clear();
            taskManager_UpdateTaskList();
            mTasks_name.Sort();
            MessageBox.Show(String.Join(", >>", mTasks_name.ToArray()));
        }
        protected static List<string> mTasks_name = new List<string>();

        public static bool taskManager_IsExistTask(string serviceName)
        {
            using (TaskService ts = new TaskService())
            {
                if (ts.RootFolder.Tasks.Exists(serviceName))
                {
                    return true;
                }
            }
            return false;

        }

        public static string taskManager_AddTask(string serviceName, string description)
        {
            using (TaskService ts = new TaskService())
            {
                if (ts.RootFolder.Tasks.Exists(serviceName))
                {
                    return "이미 '" + serviceName + "' 가 존재합니다.";
                }

                using (TaskDefinition td = ts.NewTask())
                {
                    td.RegistrationInfo.Description = description;

                    td.Principal.UserId = string.Concat(Environment.UserDomainName, "\\", Environment.UserName); //계정
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.RegistrationInfo.Author = "ElElectric";

                    LogonTrigger lt = new LogonTrigger(); //로그인할때 실행
                    lt.Enabled = true;
                    td.Triggers.Add(lt);

                    td.Actions.Add(new ExecAction(AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName, null, AppDomain.CurrentDomain.BaseDirectory)); //프로그램, 인자등록.

                    ts.RootFolder.RegisterTaskDefinition(serviceName, td); //MyScheduler란 이름으로 등록.
                    return "'" + serviceName + "' 을 등록하였습니다.";
                }
            }
        }

        public static string taskManager_RemoveTask(string serviceName)
        {
            using (TaskService ts = new TaskService())
            {
                if (ts.RootFolder.Tasks.Exists(serviceName))
                {
                    ts.RootFolder.DeleteTask(serviceName);
                    return "'" + serviceName + "' 제거 완료";
                }
                else
                {
                    return "'" + serviceName + "' 없음";
                }
            }
        }
    }
}
