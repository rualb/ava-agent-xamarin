using System;
using System.Collections.Generic;
using System.Text;

namespace AvaGE.MobControl.ControlsTools.UserMessanger
{
    public interface IUserMessanger
    {
        /// <summary>
        /// add additional msg
        /// </summary>
        /// <param name="text"></param>
        void add(string text);
        /// <summary>
        /// clear prev and begin new 
        /// </summary>
        /// <param name="text"></param>
        void set(string text);
        /// <summary>
        /// clear messages
        /// </summary>
        void clear( );
        void write();
    }
}
