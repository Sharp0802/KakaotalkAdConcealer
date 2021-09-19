#include "Win32.h"
#include <string>
#include <vcclr.h>

using namespace std;

using namespace System::ComponentModel;
using namespace System::Runtime::InteropServices;
using namespace KakaotalkAdConcealer::Native;

typedef BOOL(__stdcall* CallbackPointer)(HWND, LPARAM);

[UnmanagedFunctionPointerAttribute(CallingConvention::StdCall)]
private delegate BOOL CallbackDelegate(HWND, LPARAM);

private ref class CallbackConverter sealed : IDisposable
{
private:
	property Func<IntPtr, IntPtr, bool>^ Callback;
	property GCHandle Handle;

public:
	BOOL Call(const HWND handle, const LPARAM param)
	{
		return CallbackConverter::Callback(IntPtr(handle), IntPtr(param));
	}

	CallbackConverter(Func<IntPtr, IntPtr, bool>^ callback)
	{
		Callback = callback;
		Handle = GCHandle::Alloc(Callback, GCHandleType::Pinned);
	}

	~CallbackConverter()
	{
		if (Handle.IsAllocated)
			Handle.Free();
	}
};

void Win32::EnumChildWindows(IntPtr window, Func<IntPtr, IntPtr, bool>^ callback, IntPtr param)
{
	auto cvt = gcnew CallbackConverter(callback);
	auto del = gcnew CallbackDelegate(cvt, &CallbackConverter::Call);
	::EnumChildWindows(
		static_cast<HWND>(window.ToPointer()),
		static_cast<CallbackPointer>(Marshal::GetFunctionPointerForDelegate(del).ToPointer()),
		static_cast<LPARAM>(param.ToInt64()));
}

IntPtr Win32::FindWindow(IntPtr parent, IntPtr child, String^ cls, String^ window)
{
	const pin_ptr<const wchar_t> nCls = PtrToStringChars(cls);
	const pin_ptr<const wchar_t> nWindow = PtrToStringChars(window);
	const auto res = ::FindWindowExW(
		static_cast<HWND>(parent.ToPointer()),
		static_cast<HWND>(child.ToPointer()),
		nCls,
		nWindow);
	return IntPtr(res);
}

String^ Win32::GetClassName(IntPtr window)
{
	wchar_t text[256];
	::GetClassNameW(static_cast<HWND>(window.ToPointer()), text, 256);
	return gcnew String(text);
}

IntPtr Win32::GetParent(IntPtr window)
{
	const auto res = ::GetParent(static_cast<HWND>(window.ToPointer()));
	return IntPtr(res);
}

String^ Win32::GetWindowText(IntPtr window)
{
	wchar_t text[256];
	::GetWindowTextW(static_cast<HWND>(window.ToPointer()), text, 256);
	return gcnew String(text);
}

Rect Win32::GetWindowRect(IntPtr window)
{
	tagRECT rect;
	::GetWindowRect(static_cast<HWND>(window.ToPointer()), &rect);
	return Rect(rect.top, rect.left, rect.bottom, rect.right);
}

void Win32::ShowWindow(IntPtr window, ShowingFlag flag)
{
	::ShowWindow(
		static_cast<HWND>(window.ToPointer()),
		static_cast<int32_t>(flag));
}

IntPtr Win32::SendMessage(IntPtr handle, const UINT32 msg)
{
	auto res = ::SendMessageW(
		static_cast<HWND>(handle.ToPointer()),
		msg,
		0, 0);
	return static_cast<IntPtr>(res);
}

void Win32::SetWindowPos(IntPtr window, IntPtr zOrder, Vector pos, Vector size, SizingFlag flag)
{
	::SetWindowPos(
		static_cast<HWND>(window.ToPointer()),
		static_cast<HWND>(zOrder.ToPointer()),
		pos.X, pos.Y, size.X, size.Y,
		static_cast<uint32_t>(flag));
}

void Win32::UpdateWindow(IntPtr window)
{
	::UpdateWindow(static_cast<HWND>(window.ToPointer()));
}
