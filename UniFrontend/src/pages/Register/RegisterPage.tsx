import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../api";
import "./RegisterPage.css";

export default function RegisterPage() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const validatePassword = (password: string) => {
    return password.length >= 8 && /\d/.test(password);
  };

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!username || !password) {
      setError("Please fill all fields");
      return;
    }

    if (!validatePassword(password)) {
      setError(
        "Password must be at least 8 characters and contain at least one digit"
      );
      return;
    }

    try {
      await api.post("/auth/register", { username, password });
      navigate("/login");

    } catch (error: unknown) {
        if (error) {
        setError("Registration failed");
      }
    }
  };

  return (
    <div className="register-container">
      <form className="register-form" onSubmit={handleRegister}>
        <h2>Register</h2>
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
        <button type="submit">Register</button>
        <button type="button" onClick={() => navigate("/login")}>
          Go To Login
        </button>
      </form>
    </div>
  );
}
