"use client";

import { useEffect, useState } from "react";
import { useRouter, useSearchParams } from "next/navigation";
import axios from "axios";
import { Toaster } from "@/components/ui/sonner"
import { Button } from "@/components/ui/button"
import { Skeleton } from "@/components/ui/skeleton"

import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
  } from "@/components/ui/table"
import { toast } from "sonner";

  

interface User {
  id: string;
  userName: string;
  email: string;
  emailConfirmed: boolean;
}

export default function UserList() {
  const router = useRouter();
  const [users, setUsers] = useState<User[]>([]);
  const token = localStorage.getItem("token");

  console.log(token);
  
  useEffect(() => {
    if (!token) {
      alert("Unauthorized! Please log in.");
      router.push("/auth/login");
      return;
    }

    const fetchUsers = async () => {
      try {
        const response = await axios.get("https://localhost:7261/api/admin/getAllUsers", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        setUsers(response.data.body);
      } catch (error: any) {
        alert(error?.response?.data?.message || "Failed to fetch users");
      }
    };

    fetchUsers();
  }, [token, router]);

  const handleUpdate = (email: string) => {
    router.push(`/admin/update-user/${email}`);
  };

const handleBack=()=>{
  router.push("/protected/home")
}

  
  const handleDelete = async (email: string) => {

    console.log(email);
    toast("Confirm Delete", {
      description: "Are you sure you want to delete this user?",
      duration: 3000,
      style: {
        backgroundColor: "#000000",
        color: "#FFFFFF",
      },
      action: (
        <button
          onClick={async () => {
            try {
              await axios.delete(`https://localhost:7261/api/user/deleteUser/${email}`, {
                headers: {
                  Authorization: `Bearer ${token}`,
                },
              });
              toast("Success", {
                description: "User deleted successfully!",
                style: {
                  backgroundColor: "#000000",
                  color: "#FFFFFF",
                },
              });

            setUsers(users.filter((user) => user.email !== email));

            } catch (error: any) {
              toast("Error", {
                description: error?.response?.data?.message || "Failed to delete user",
                style: {
                  backgroundColor: "#000000",
                  color: "#FFFFFF",
                },
              });
            }
          }}
          style={{
            backgroundColor: "#FF0000",
            color: "#FFFFFF",
            padding: "6px 6px",
            borderRadius: "8px",
            border: "none",
            cursor: "pointer",
            marginLeft: "25px",
          }}
        >
          Delete
        </button>
      ),
    });
  };
  




  return (
    <div className="p-6">
      <Toaster/>
    <h1 className="text-3xl font-bold mb-6">Admin - User List</h1>
      {users.length === 0 ? (
        <>
       <div className="space-y-4">
  <Skeleton className="h-8 w-full rounded-xl" /> {/* Table Header Skeleton */}
  <div className="space-y-2">
    {Array.from({ length: 3 }).map((_, index) => (
      <div key={index} className="flex space-x-4">
        <Skeleton className="h-6 w-[30%] rounded-lg" />
        <Skeleton className="h-6 w-[30%] rounded-lg" />
        <Skeleton className="h-6 w-[30%] rounded-lg" />
      </div>
    ))}
  </div>
</div></>
      ) : (
        <>
          <Table>
      <TableCaption>List of registered users and their details.</TableCaption>
      <TableHeader>
        <TableRow>
          <TableHead className="w-[150px]">Username</TableHead>
          <TableHead>Email</TableHead>
          <TableHead>Email Confirmed</TableHead>
          <TableHead className="text-center">Actions</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {users.map((user: any) => (
          <TableRow key={user.id}>
            <TableCell className="font-medium">{user.userName}</TableCell>
            <TableCell>{user.email}</TableCell>
            <TableCell>{user.emailConfirmed ? "Yes" : "No"}</TableCell>
            <TableCell className="text-center">
              <button
                onClick={() => handleUpdate(user.email)}
                className="bg-yellow-500 text-white px-4 py-2 rounded-lg mr-2 hover:bg-yellow-600"
              >
                Update
              </button>
              <button
                onClick={() => handleDelete(user.email)}
                className="bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600"
              >
                Delete
              </button>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>

    <Button onClick={handleBack} className="bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600">Back</Button>


        </>
      )}
    </div>
  );
}
