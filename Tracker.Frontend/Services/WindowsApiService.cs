using System.Runtime.InteropServices;

namespace Tracker.Frontend.Services;

public sealed class WindowsApiService
{
    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;

    private IntPtr windowHandle;

    public void HideMainWindow()
    {
        if (windowHandle == IntPtr.Zero)
        {
            windowHandle = FindMainWindowHandle();
        }

        if (!IsWindow(windowHandle))
        {
            return;
        }

        ShowWindow(windowHandle, SW_HIDE);
    }

    public void ShowMainWindow()
    {
        if (!IsWindow(windowHandle))
        {
            return;
        }

        ShowWindow(windowHandle, SW_SHOW);
    }

    private static IntPtr FindMainWindowHandle()
    {
        var currentProcessId = (uint) Environment.ProcessId;
        IntPtr result = IntPtr.Zero;

        EnumWindows((handle, _) =>
        {
            GetWindowThreadProcessId(handle, out uint processId);

            if (processId != currentProcessId || !IsWindowVisible(handle))
            {
                return true;
            }

            result = handle;
            return false;
        }, IntPtr.Zero);

        return result;
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
}
