using System;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using FakeGnomoria;

namespace FakeMod
{
    class Program
    {

        //Using Main to inject the mod
        static void Main(string[] args)
        {
            //MODIFY THIS PATH TO THE CORRECT LOCATION
            AssemblyDefinition gnomoria = 
                AssemblyDefinition.ReadAssembly("J:/repos/C#/NewAttempt/PracticeInjection/FakeMod/bin/Release/FakeGnomoria.exe");

            TypeDefinition fakeType = gnomoria.MainModule.GetType("FakeGnomoria.FakeObject");
            MethodDefinition pasteNums = fakeType.Methods.Single(method => method.Name == "PasteNumbers");
            ParameterDefinition this_FakeObject = pasteNums.Body.ThisParameter;

            //Create instructions for injection
            MethodBase startFakeModMethodBase =
                typeof (TheFakeMod).GetMethod("StartFakeMod", new Type[] {typeof (FakeObject) });
            MethodReference startFakeModMethodReference = pasteNums.Module.Import(startFakeModMethodBase);

            //Get the ILProcesser for pasteNums
            ILProcessor il = pasteNums.Body.GetILProcessor();

            //Loads the 'this' FakeObject as an argument
            Instruction getThis = il.Create(OpCodes.Ldarg, this_FakeObject);

            //Calls the StartFakeMod method with the argument loaded above
            Instruction callstartFakeMod = il.Create(OpCodes.Call, startFakeModMethodReference);

            //Gets last instruction of pasteNums (which is the Return call)
            Instruction pasteNumsIns = pasteNums.Body.Instructions.Last();

            //Inserts new call before return statement in pasteNums
            il.InsertBefore(pasteNumsIns, getThis);
            il.InsertAfter(getThis, callstartFakeMod);

            //Overwrite old assembly MODIFY THIS PATH TO THE CORRECT LOCATION
            gnomoria.Write("J:/repos/C#/NewAttempt/PracticeInjection/FakeMod/bin/Release/FakeGnomoriaModded.exe");

        }
    }

    public class TheFakeMod
    {
        //        public override IEnumerable<IModification> Modifications
        //        {
        //            get
        //            {
        //                yield return new MethodHook(
        //                    typeof(GnomanEmpire).GetMethod("FinishLoadingGame"),
        //                    Method.Of<GnomanEmpire>(TheFakeMod)
        //                    );
        //            }
        //        }
        //        public override string Author
        //        {
        //            get
        //            {
        //                return "ItsComcastic";
        //            }
        //        }
        //        public override string Description
        //        {
        //            get
        //            {
        //                return "";
        //            }
        //        }

        public static void StartFakeMod(FakeObject self)
        {
            Console.Out.WriteLine("The number stored in FakeObject is: " + self.Num);
        }

    }
}
