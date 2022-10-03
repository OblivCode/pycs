using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;


namespace pycs.modules
{
    public class threading 
    {
        public static int active_count() => Process.GetCurrentProcess().Threads.Count;

        public int get_ident() => System.Threading.Thread.CurrentThread.ManagedThreadId;

        public static Thread current_thread()
        {
            return new Thread(System.Threading.Thread.CurrentThread);
        }

        public class Thread
        {
            public string name { get { return This.Name; } }
            public bool daemon { get { return This.IsBackground;  } }
            private System.Threading.Thread This = null;

            /// <summary>
            /// arguments array pass as strings
            /// </summary>>
           
            public Thread(ThreadStart target = null, string name = null, bool daemon = false)
            {
                 This =  new System.Threading.Thread(target);
                init(name, daemon);
            }

           
            public Thread(Action<object> target = null, object arg = null, string name = null, bool daemon = false)
            {
                This = new System.Threading.Thread(() => target(arg));
                init(name, daemon);
            }

           
            private void init(string name, bool daemon)
            {
                This.Name = name;
                This.IsBackground = daemon;
                AppDomain.CurrentDomain.ProcessExit += (v1, v2) =>
                {
                    if (This.ThreadState == System.Threading.ThreadState.Stopped) return;
                    if (This.IsBackground)
                        if (This.IsAlive)
                            This.Abort();
                };
            }
            public Thread(System.Threading.Thread thread)
            {
                This = thread;
                AppDomain.CurrentDomain.ProcessExit += (v1, v2) =>
                {
                    if (This.IsBackground)
                        if (This.IsAlive)
                            This.Abort();
                };
            }

            /// <summary>
            ///  same as run()
            /// </summary>
            public void start()
            {
               run();

            }

            public void run()
            {
                This.Start();
            }


            public void join(double timeout = -1)
            {
                if (timeout > 0)
                {
                    int ms = int.Parse(math.trunc(timeout * 1000).ToString());
                    This.Join(ms);
                }
                else
                    This.Join();
            }
           
            public void setDaemon(bool daemonic) => This.IsBackground = daemonic;
            public string getName() => This.Name;
            public string setName(string name) => This.Name = name;
            public int get_ident() => This.ManagedThreadId;
            public bool isDaemon() => This.IsBackground;
            public bool is_alive() => This.IsAlive;
        }
    }
}
