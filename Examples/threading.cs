using static pycs.pycs;
using pycs.modules;

namespace my_app
{
    class Program { 
        static void Main(){
            var t1 = new threading.Thread(func1);
            var t2 = new threading.Thread(func2);
            t2.start();
            t1.start();
        
        }

        static void func2(){
            print("Hello");
        }
        static void func1() {
            print("World");
        }
    }

}