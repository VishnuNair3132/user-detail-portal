"use client";

import { useEffect, useState } from "react";
import { useRouter, useParams } from "next/navigation";
import axios from "axios";
import { toast, Toaster } from "sonner";

export default function UpdatePage() {
  const router = useRouter();
  const { email } = useParams();
  const [firstname, setFirstname] = useState("");
  const [lastname, setLastname] = useState("");
  const [error, setError] = useState("");
  const token = localStorage.getItem("token");


  type UpdateRequest ={
    firstname:string,
    lastname:string
  }


  useEffect(() => {
    if (!token) {
      alert("Unauthorized. Please log in.");
      router.push("/auth/login");
      return;
    }

    const fetchUser = async () => {
      try {
        const response = await axios.get(`https://localhost:7261/api/user/getUserByEmail/${email}`, {
          headers: { Authorization: `Bearer ${token}` },
        });
        setFirstname(response.data.body.firstname);
        setLastname(response.data.body.lastname);
      } catch (error:any) {
        alert(error.response?.data?.message || "Failed to fetch user data.");
      }
    };

    fetchUser();
  }, [email, token, router]);

  const handleUpdate = async () => {
    if (!firstname.trim() || !lastname.trim()) {
      setError("Firstname and Lastname cannot be empty.");
      return;
    }


    const updateRequest:UpdateRequest={
      firstname,
      lastname
    }

    try {
      await axios.post(`https://localhost:7261/api/user/updateUser/${email}`, updateRequest, {
        headers: { Authorization: `Bearer ${token}` },
      });


      toast("Profile Updated", {
        description: "User information updated successfully",
        style: {
            backgroundColor: "#000000",
            color: "#FFFFFF",
        },
    });
    } catch (error: any) {
      toast("Error", {
          description: `${error}`,
          style: {
              backgroundColor: "#000000",
              color: "#FFFFFF",
          },
      });;
  }
  };

  const handleBack = () => {
    router.push("/admin/userList");
  };

  return (
    <div className="flex flex-col items-center justify-center min-h-screen p-4 bg-gray-50">
    <h1 className="text-2xl font-bold mb-6">Update User</h1>

    {error && <p className="text-red-500 mb-4">{error}</p>}

    <div className="bg-white shadow-lg rounded-xl p-8 w-full max-w-md">
      <label className="mb-2 block text-gray-700 font-semibold">Firstname:</label>
      <input
        type="text"
        value={firstname}
        onChange={(e) => setFirstname(e.target.value)}
        className="p-2 border border-gray-300 rounded mb-4 w-full focus:outline-none focus:ring-2 focus:ring-blue-500"
      />

      <label className="mb-2 block text-gray-700 font-semibold">Lastname:</label>
      <input
        type="text"
        value={lastname}
        onChange={(e) => setLastname(e.target.value)}
        className="p-2 border border-gray-300 rounded mb-4 w-full focus:outline-none focus:ring-2 focus:ring-blue-500"
      />

      <div className="flex space-x-4">
        <button onClick={handleUpdate} className="bg-blue-500 text-white px-6 py-2 rounded-lg hover:bg-blue-600">
          Update User
        </button>

        <button onClick={handleBack} className="bg-gray-500 text-white px-6 py-2 rounded-lg hover:bg-gray-600">
          Back
        </button>
        <Toaster/>
      </div>
    </div>
  </div>
    );
}
