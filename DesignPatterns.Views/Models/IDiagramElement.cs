using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Views
{
    public interface IDiagramElement
    {
        bool Compare(IDiagramElement example);
    }
}
