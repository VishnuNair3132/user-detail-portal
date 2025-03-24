"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { jwtDecode } from "jwt-decode";
import { toast } from "sonner";
 import { Toaster } from "@/components/ui/sonner";
import { Button } from "@/components/ui/button";


interface JwtPayload {
    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string;
}

export default function Navbar() {
    const router = useRouter();
    const [role, setRole] = useState<string | null>(null);

    useEffect(() => {
        const token = localStorage.getItem("token");

        if (token) {
            try {
                const decodedToken: JwtPayload = jwtDecode(token);
                setRole(decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
                console.log(role)
            } catch (error) {
                console.error("Invalid token:", error);
            }
        }
    }, []);

    
  const navigateWithToken = (path: string) => {
    const token = localStorage.getItem("token");
    if (token) {
      router.push(`${path}?token=${encodeURIComponent(token)}`);
    }
  };

    const handleLogout = () => {
        toast("Confirm Logout", {
          description: "Are you sure you want to logout?",
          duration: 5000,
          style: {
            backgroundColor: "#000000", // Black background
            color: "#FFFFFF",           // White text
          },
          action: (
            <button
              onClick={() => {
                localStorage.removeItem("token");
                localStorage.removeItem("toastShown")
                toast("Logged Out", {
                  description: "You have been successfully logged out.",
                  style: {
                    backgroundColor: "#000000",
                    color: "#FFFFFF",
                    marginRight:"18px",
                  },
                });
                router.push("/auth/login");
              }}
              style={{
                backgroundColor: "#FF0000", // Red button
                color: "#FFFFFF",           // White text
                padding: "8px 16px",
                borderRadius: "8px",
                border: "none",
                cursor: "pointer",
                marginLeft: "25px",
              }}
            >
              Logout
            </button>
          ),
        });
      };
      
      
    return (
        <nav className="bg-blue-600 p-4 text-white flex justify-between items-center">
            <h1
                className="text-2xl font-bold cursor-pointer"
                onClick={() => router.push("/")}
            >
                User Portal
            </h1>
            <div className="flex items-center gap-4">
                {role === "User" && (
                    <button
                        onClick={() => navigateWithToken("/protected/userDetails")}
                        className="bg-yellow-500 px-4 py-2 rounded-lg hover:bg-yellow-600 transition"
                    >
                        Update Profile
                    </button>
                )}

                {role === "Admin" && (
                    <button
                        onClick={() => router.push("/admin/userList")}
                        className="bg-purple-500 px-4 py-2 rounded-lg hover:bg-purple-600 transition"
                    >
                        User List
                    </button>
                )}

                <Toaster/>
                <button
                    onClick={handleLogout}
                    className="bg-red-500 px-4 py-2 rounded-lg hover:bg-red-600 transition"
                >
                    Logout
                </button>
            </div>
        </nav>
    );
}
