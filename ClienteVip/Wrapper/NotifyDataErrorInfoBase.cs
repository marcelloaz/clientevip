﻿using ClienteVip.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ClienteVip.Wrapper
{
  public class NotifyDataErrorInfoBase : Observable, INotifyDataErrorInfo
  {
    protected readonly Dictionary<string, List<string>> Errors;

    protected NotifyDataErrorInfoBase()
    {
      Errors = new Dictionary<string, List<string>>();
    }

    public bool HasErrors => Errors.Any();

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public IEnumerable GetErrors(string propertyName)
    {
      return propertyName != null && Errors.ContainsKey(propertyName)
        ? Errors[propertyName]
        : Enumerable.Empty<string>();
    }

    protected virtual void OnErrorsChanged(string propertyName)
    {
      ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    protected void ClearErrors()
    {
      foreach (var propertyName in Errors.Keys.ToList())
      {
        Errors.Remove(propertyName);
        OnErrorsChanged(propertyName);
      }
    }
  }
}
