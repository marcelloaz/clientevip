﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteVip.Wrapper
{
  public interface IValidatableTrackingObject :
    IRevertibleChangeTracking,
    INotifyPropertyChanged
  {
    bool IsValid { get; }
  }
}
