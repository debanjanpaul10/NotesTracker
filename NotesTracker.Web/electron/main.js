const { app, BrowserWindow, Menu } = require("electron");
const {
  WINDOW_WIDTH,
  WINDOW_HEIGHT,
  APP_URL,
} = require("./helpers/constants.electron");

/**
 * Creates the electron desktop application window.
 * Sets the `window height`, `window width` and the `web base url`.
 */
function createWindow() {
  const win = new BrowserWindow({
    width: WINDOW_WIDTH,
    height: WINDOW_HEIGHT,
    webPreferences: {
      nodeIntegration: true,
    },
  });

  win.loadURL(APP_URL);

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
