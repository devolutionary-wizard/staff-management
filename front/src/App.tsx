import { ThemeProvider } from '@mui/material'
import { ThemeSettings } from './themes/Theme';
import { useRoutes } from "react-router-dom";
import Router from './routes';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

function App() {
  const theme = ThemeSettings();
  const queryClient = new QueryClient()
  const router = useRoutes(Router)
  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider theme={theme}>
        {router}
      </ThemeProvider>
    </QueryClientProvider>

  )
}

export default App
