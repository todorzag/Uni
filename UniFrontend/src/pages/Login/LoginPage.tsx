import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api';
import './LoginPage.css';
import { useAuth } from '../../hooks/AuthHook';
import type { AxiosError } from 'axios';

export default function LoginPage() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!username || !password) {
      setError("All fields are required.");
      return;
    }

    try {
      const res = await api.post("/auth/login", { username, password });
      if (res.data && res.data.token) {
        login(res.data.token);
        navigate("/computers");
      } else {
        setError("Login failed. No token received.");
      }
    } catch (err: unknown) {
      const error = err as AxiosError;

      if (error.response && error.response.status === 401) {
        setError("Incorrect username or password.");
      } else if (error.response && error.response.status === 404) {
        setError("User not found.");
      } else {
        setError("Something went wrong. Please try again later.");
      }
    }
  };

  return (
    <div className="login-container">
      <form className="login-form" onSubmit={handleLogin}>
        <h2>Login</h2>
        {error && <p className="error">{error}</p>}
        <input
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          placeholder="Username"
        />
        <input
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          type="password"
          placeholder="Password"
        />
        <button type="submit">Login</button>
        <button type="button" onClick={() => navigate("/register")}>Go To Register</button>
      </form>
    </div>
  );
}