//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;

namespace Noesis
{

public class SizeChangedInfo : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal SizeChangedInfo(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(SizeChangedInfo obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~SizeChangedInfo() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NoesisGUI_PINVOKE.delete_SizeChangedInfo(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public SizeChangedInfo(UIElement element, Size previousSize, bool widthChanged, bool heightChanged)
    : this(CreateHelper(element.RenderSize, previousSize, widthChanged, heightChanged), true) {
  }

  public Size NewSize {
    get {
      IntPtr ret = NoesisGUI_PINVOKE.SizeChangedInfo_NewSize_get(swigCPtr);
      if (ret != IntPtr.Zero) {
        return Marshal.PtrToStructure<Size>(ret);
      }
      else {
        return new Size();
      }
    }

  }

  public Size PreviousSize {
    get {
      IntPtr ret = NoesisGUI_PINVOKE.SizeChangedInfo_PreviousSize_get(swigCPtr);
      if (ret != IntPtr.Zero) {
        return Marshal.PtrToStructure<Size>(ret);
      }
      else {
        return new Size();
      }
    }

  }

  public bool WidthChanged {
    get {
      bool ret = NoesisGUI_PINVOKE.SizeChangedInfo_WidthChanged_get(swigCPtr);
      return ret;
    } 
  }

  public bool HeightChanged {
    get {
      bool ret = NoesisGUI_PINVOKE.SizeChangedInfo_HeightChanged_get(swigCPtr);
      return ret;
    } 
  }

  private static IntPtr CreateHelper(Size newSize, Size previousSize, bool widthChanged, bool heightChanged) {
    IntPtr ret = NoesisGUI_PINVOKE.SizeChangedInfo_CreateHelper(ref newSize, ref previousSize, widthChanged, heightChanged);
    return ret;
  }

  public SizeChangedInfo() : this(NoesisGUI_PINVOKE.new_SizeChangedInfo(), true) {
  }

}

}
