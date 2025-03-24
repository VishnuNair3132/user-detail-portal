"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import axios from "axios";

export default function LoginPage() {
  const router = useRouter();
  const [formData, setFormData] = useState({
    username: "",
    password: "",
  });
  const [error, setError] = useState("");

  const checkToken = () => {
    const token = localStorage.getItem("token");


    if (token) {
      router.push("/protected/home");
    }
  };


  checkToken();



  const handleChange = (e:any) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e:any) => {
    e.preventDefault();
    setError("");
    try {
      const response = await axios.post(
        "https://localhost:7261/api/user/login",
        formData,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      const token = response.data.token;
      if (token) {
        localStorage.setItem("token", token);
        console.log(token);
        router.push("/protected/home");
      } else {
        throw new Error("No token received");
      }
    } catch (error:any) {
      setError(error.response?.data?.message || "Invalid username or password");
    }
  };

  return (
    <div className="flex justify-center items-center h-screen">
      <form onSubmit={handleSubmit} className="p-6 bg-white rounded-lg shadow-2xl w-96">
        <h2 className="text-2xl font-bold mb-4 text-center">Login</h2>

        {error && <p className="text-red-500 text-center mb-4">{error}</p>}

        <input
          type="text"
          name="username"
          placeholder="Username"
          value={formData.username}
          onChange={handleChange}
          className="w-full p-2 mb-3 border rounded focus:outline focus:outline-sky-500"
          required
        />

        <input
          type="password"
          name="password"
          placeholder="Password"
          value={formData.password}
          onChange={handleChange}
          className="w-full p-2 mb-3 border rounded focus:outline focus:outline-sky-500"
          required
        />

        <button
          type="submit"
          className="w-full bg-blue-500 text-white p-2 rounded hover:bg-blue-600 cursor-pointer"
        >
          Login
        </button>

        <p className="text-center mt-4">
          Don't have an account?{" "}
          <a href="/auth/register" className="text-blue-500 hover:underline">
            Register here
          </a>
        </p>
      </form>
    </div>
  );
}
