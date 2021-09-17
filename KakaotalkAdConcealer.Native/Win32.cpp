#include "Win32.h"
#include <string>

using namespace System::ComponentModel;
using namespace System::Runtime::InteropServices;
using namespace KakaotalkAdConcealer::Native;

void Validate(int result, const std::string name)
{
	auto msg = gcnew String(("Runtime exception occurred while executing method '" + name + "'.").c_str());
	if (result != 0)
		throw gcnew Win32Exception(result, msg);
}

typedef BOOL(__stdcall* CallbackPointer)(HWND, LPARAM);

[UnmanagedFunctionPointerAttribute(CallingConvention::StdCall)]
private delegate BOOL CallbackDelegate(HWND, LPARAM);

private ref class CallbackConverter sealed : IDisposable
{
private:
	property Func<IntPtr, IntPtr, bool>^ Callback;
	property GCHandle Handle;
	
public:
	BOOL Call(HWND handle, LPARAM param)
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
	auto res = ::EnumChildWindows(
		static_cast<HWND>(window.ToPointer()),
		static_cast<CallbackPointer>(Marshal::GetFunctionPointerForDelegate(del).ToPointer()),
		static_cast<LPARAM>(param.ToInt64()));
	Validate(res, __func__);
}

IntPtr Win32::FindWindow(IntPtr parent, IntPtr child, String^ cls, String^ window)
{
	pin_ptr<const wchar_t> nCls = PtrToStringChars(cls);
	pin_ptr<const wchar_t> nWindow = PtrToStringChars(window);
	auto res = ::FindWindowExW(
		static_cast<HWND>(parent.ToPointer()),
		static_cast<HWND>(child.ToPointer()),
		static_cast<LPCWSTR>(nCls),
		static_cast<LPCWSTR>(nWindow));
	return IntPtr(res);
}

String^ Win32::GetClassName(IntPtr window)
{
	wchar_t text[256];
	auto res = ::GetClassNameW(static_cast<HWND>(window.ToPointer()), text, 256);
	Validate(res, __func__);
	return gcnew String(text);
}

IntPtr Win32::GetParent(IntPtr window)
{
	auto res = ::GetParent(static_cast<HWND>(window.ToPointer()));
	return IntPtr(res);
}

String^ Win32::GetWindowText(IntPtr window)
{
	wchar_t text[256];
	auto res = ::GetWindowTextW(static_cast<HWND>(window.ToPointer()), text, 256);
	Validate(res, __func__);
	return gcnew String(text);
}

Rect Win32::GetWindowRect(IntPtr window)
{
	tagRECT rect;
	auto res = ::GetWindowRect(static_cast<HWND>(window.ToPointer()), &rect);
	Validate(res, __func__);
	return Rect(rect.top, rect.left, rect.bottom, rect.right);
}

void Win32::ShowWindow(IntPtr window, ShowingFlag flag)
{
	auto res = ::ShowWindow(
		static_cast<HWND>(window.ToPointer()),
		static_cast<int32_t>(flag));
	Validate(res, __func__);
}

IntPtr Win32::SendMessage(IntPtr handle, UINT32 msg)
{
	auto res = ::SendMessageW(
		static_cast<HWND>(handle.ToPointer()),
		msg,
		0, 0);
	return static_cast<IntPtr>(res);
}

void Win32::SetWindowPos(IntPtr window, IntPtr zOrder, Vector pos, Vector size, SizingFlag flag)
{
	auto res = ::SetWindowPos(
		static_cast<HWND>(window.ToPointer()),
		static_cast<HWND>(zOrder.ToPointer()),
		pos.X, pos.Y, size.X, size.Y,
		static_cast<uint32_t>(flag));
	Validate(res, __func__);
}

void Win32::UpdateWindow(IntPtr window)
{
	auto res = ::UpdateWindow(static_cast<HWND>(window.ToPointer()));
	Validate(res, __func__);
}
