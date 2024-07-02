import { ThemeProvider } from '@mui/material'
import { ThemeSettings } from './themes/Theme';
import { useRoutes } from "react-router-dom";
import Router from './routes';

function App() {
  const theme = ThemeSettings();
  const router = useRoutes(Router)
  return (
    <ThemeProvider theme={theme}>
      {router}
    </ThemeProvider>
  )
}

export default App
