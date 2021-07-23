#include "Win32.h"

using namespace KakaotalkAdConcealer::Native;

void Validate(int result)
{
    if (result == 0 && Win32::ExceptionWhenFail)
        throw gcnew ExternalException();
}

private ref class CallbackConverter abstract sealed
{
private:
    static Object^ _locker = gcnew Object();
public:
    static property Object^ Locker { Object^ get() { return _locker; } }
    static property Func<IntPtr, IntPtr, bool>^ Callback;
};
BOOL __stdcall EnumChildWindowsCallbackCall(HWND handle, LPARAM param)
{
    return CallbackConverter::Callback(IntPtr(handle), IntPtr(param));
}
void Win32::EnumChildWindows(IntPtr window, Func<IntPtr, IntPtr, bool>^ callback, IntPtr param)
{
    BOOL res;
    bool taken = false;
    try
    {
        Monitor::Enter(CallbackConverter::Locker, taken);

        CallbackConverter::Callback = callback;
        res = ::EnumChildWindows(
            static_cast<HWND>(window.ToPointer()),
            EnumChildWindowsCallbackCall,
            static_cast<LPARAM>(param.ToInt64()));
    }
    finally
    {
        if (taken)
            Monitor::Exit(CallbackConverter::Locker);
    }
    Validate(res);
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
    Validate(res);
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
    Validate(res);
    return gcnew String(text);
}

Rect Win32::GetWindowRect(IntPtr window)
{
    tagRECT rect;
    auto res = ::GetWindowRect(static_cast<HWND>(window.ToPointer()), &rect);
    Validate(res);
    return Rect(rect.top, rect.left, rect.bottom, rect.right);
}

void Win32::ShowWindow(IntPtr window, ShowingFlag flag)
{
    auto res = ::ShowWindow(
        static_cast<HWND>(window.ToPointer()),
        static_cast<int32_t>(flag));
    Validate(res);
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
    Validate(res);
}

void Win32::UpdateWindow(IntPtr window)
{
    auto res = ::UpdateWindow(static_cast<HWND>(window.ToPointer()));
    Validate(res);
}
