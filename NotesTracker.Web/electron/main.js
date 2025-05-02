const { app, BrowserWindow, Menu } = require("electron");

function createWindow() {
  const win = new BrowserWindow({
    width: 1920,
    height: 1080,
    webPreferences: {
      nodeIntegration: true,
    },
  });

  win.loadURL("http://localhost:4200/");

  // Disable the default Menu.
  Menu.setApplicationMenu(null);

  // Prevent the developer tools from opening.
  win.webContents.on("devtools-opened", () => {
    win.webContents.closeDevTools();
  });
}

app.on("ready", createWindow);
app.on("window-all-closed", () => {
  if (process.platform !== "darwin") {
    app.quit();
  }
});

app.on("activate", () => {
  if (BrowserWindow.getAllWindows().length === 0) {
    createWindow();
  }
});
