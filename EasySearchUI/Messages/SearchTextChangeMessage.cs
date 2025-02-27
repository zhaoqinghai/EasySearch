using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySearchUI.Messages
{
    public class SearchTextChangeMessage : ValueChangedMessage<string?>
    {
        public SearchTextChangeMessage(string? value) : base(value)
        {
        }
    }
}