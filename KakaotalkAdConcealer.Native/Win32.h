#pragma once

#pragma comment(lib, "user32.lib")

#include <Windows.h>
#include <vcclr.h>

#include "Rect.h"
#include "ShowingFlag.h"
#include "SizingFlag.h"
#include "Vector.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Threading;

using Out = OutAttribute;

namespace KakaotalkAdConcealer::Native
{
	public ref class Win32 abstract sealed
	{
	private:
		static Object^ _locker;
		static bool _exceptionWhenFail;

	public:
		static property bool ExceptionWhenFail
		{
			bool get() 
			{
				return _exceptionWhenFail;
			}
			void set(bool v)
			{
				bool taken = false;
				try
				{
					Monitor::Enter(_locker, taken);
					_exceptionWhenFail = v;
				}
				finally
				{
					if (taken)
						Monitor::Exit(_locker);
				}
			}
		}

#undef EnumChildWindows
		static void EnumChildWindows(IntPtr window, Func<IntPtr, IntPtr, bool>^ callback, IntPtr param);
#undef FindWindow
		static IntPtr FindWindow(IntPtr parent, IntPtr child, String^ cls, String^ window);
#undef GetClassName
		static String^ GetClassName(IntPtr window);
#undef GetParent
		static IntPtr GetParent(IntPtr window);
#undef GetWindowText
		static String^ GetWindowText(IntPtr window);
#undef GetWindowRect
		static Rect GetWindowRect(IntPtr window);
#undef ShowWindow
		static void ShowWindow(IntPtr window, ShowingFlag flag);
#undef SendMessage
		static IntPtr SendMessage(IntPtr handle, UINT32 msg);
#undef SetWindowPos
		static void SetWindowPos(IntPtr window, IntPtr zOrder, Vector pos, Vector size, SizingFlag flag);
#undef UpdateWindow
		static void UpdateWindow(IntPtr window);
	};
}
