/* UnitHelper.cs
 * 
 * 目的：
 *      辅助测试private方法
 * 使用方法如下： 
 *      NUnitHelper nHelper = new NUnitHelper();
 *      PcDal_Pd mssqlDal = new PcDal_Pd();
 *      DataTable dt = (DataTable)nHelper.InvokePMethod(typeof(PcDal_Pd), "GetPcTb", mssqlDal, new object[1] { "23" });
 * 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PayRoll.UnitTest.Helper
{
    public class NUnitHelper
    {
        //辅助方法:调用其他类的私有方法
        //InstanceClass:类的实例,Params:方法的参数实例
        public object InvokePMethod(System.Type Type, string MethodName, object InstanceClass, object[] Params)
        {
            //发现方法的属性 (Attribute) 并提供对方法元数据的访问(摘自:MSDN)
            //这里方法的属性指方法的static,virtual,final等修饰,方法的参数,方法的返回值等详细信息
            //最重要一点是通过MethodInfo可以调用方法(invoke)
            MethodInfo Method;

            //指定被搜索成员的类型,NonPublic表示搜索非公有成员,Instance表示搜索实例成员(非static)
            //所以下面这句表示搜索类型为非公有的实例成员
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            //Type为System.Reflection功能的根，也是访问元数据的主要方式。(摘自:MSDN)
            //使用Type的成员获取关于类型声明的信息,如构造函数、方法、字段、属性 (Property) 
            //和类的事件,以及在其中部署该类的模块和程序集。(摘自:MSDN)
            //Type是.net中反射的根源,就如java中的Class类.如果连类都没有,那么调用方法,得到属性,一切都无从入手.
            //GetMethod:通过方法名和搜索方式得MethodInfo
            Method = Type.GetMethod(MethodName, flags);

            //调用private方法:参数分别为类的实例和方法参数
            object result = Method.Invoke(InstanceClass, Params);

            return result;
        }




        //辅助方法:调用其他类的私有方法
        //InstanceClass:类的实例,Params:方法的参数实例, outParams:是将所有参数重新输出（暂时没有更好的办法）
        public object InvokePMethod(System.Type Type, string MethodName, object InstanceClass, object[] Params, out object[] outParams)
        {
            //发现方法的属性 (Attribute) 并提供对方法元数据的访问(摘自:MSDN)
            //这里方法的属性指方法的static,virtual,final等修饰,方法的参数,方法的返回值等详细信息
            //最重要一点是通过MethodInfo可以调用方法(invoke)
            MethodInfo Method;

            //指定被搜索成员的类型,NonPublic表示搜索非公有成员,Instance表示搜索实例成员(非static)
            //所以下面这句表示搜索类型为非公有的实例成员
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            //Type为System.Reflection功能的根，也是访问元数据的主要方式。(摘自:MSDN)
            //使用Type的成员获取关于类型声明的信息,如构造函数、方法、字段、属性 (Property) 
            //和类的事件,以及在其中部署该类的模块和程序集。(摘自:MSDN)
            //Type是.net中反射的根源,就如java中的Class类.如果连类都没有,那么调用方法,得到属性,一切都无从入手.
            //GetMethod:通过方法名和搜索方式得MethodInfo
            Method = Type.GetMethod(MethodName, flags);

            //调用private方法:参数分别为类的实例和方法参数
            object result = Method.Invoke(InstanceClass, Params);

            outParams = Params;

            return result;
        }
    }
}
