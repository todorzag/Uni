import './index.css';
import { Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './pages/Login/LoginPage';
import RegisterPage from './pages/Register/RegisterPage';
import ComputersPage from './pages/Computers/ComputersPage';
import OrdersPage from './pages/Orders/OrdersPage';
// import AdminUsersPage from './pages/AdminUsersPage';
import ComputerDetailsPage from './pages/Computers/ComputerDetailsPage';
import { useAuth } from './hooks/AuthHook';
import type { JSX } from 'react';

function ProtectedRoute({ children }: { children: JSX.Element }) {
  const { token } = useAuth();
  return token ? children : <Navigate to="/login" />;
}

function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route
        path="/computers"
        element={<ProtectedRoute><ComputersPage /></ProtectedRoute>}
      />
      <Route path="/computers/:id" element={<ProtectedRoute><ComputerDetailsPage /></ProtectedRoute>} />
      <Route
        path="/orders"
        element={<ProtectedRoute><OrdersPage /></ProtectedRoute>}
      />
      {/* <Route
        path="/admin/users"
        element={<ProtectedRoute><AdminUsersPage /></ProtectedRoute>}
      /> */}
    </Routes>
  );
}

export default App;