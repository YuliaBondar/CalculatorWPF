using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Core.Interfaces
{
    public interface IOperation 
    {
        //интерфейс это метод который должен реализовываться классами, используя интерфейс 
        double Call(params double[] args);
    }
}
