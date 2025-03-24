"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import Navbar from "./Navbar";
import { toast } from "sonner";
 import { Toaster } from "@/components/ui/sonner";

export default function HomePage() {
  const router = useRouter();
  const [username, setUsername] = useState<string | null>(null);

  // Function to decode JWT
  const decodeJWT = (token: string) => {
    try {
      const base64Url = token.split('.')[1]; // Get the payload part
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = JSON.parse(atob(base64));
      return jsonPayload;
    } catch (error) {
      console.error("Invalid token:", error);
      return null;
    }
  };

  useEffect(() => {
    const token = localStorage.getItem("token");
  
    if (!token) {
      alert("Please log in first.");
      router.push("/auth/login");
    } else {
      const decodedToken = decodeJWT(token);
      if (decodedToken) {
        const userName = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        setUsername(userName);
  
        if (!localStorage.getItem("toastShown")) {
          toast("Greetings", {
            description: `Welcome back, ${userName}!`,
            style: {
              backgroundColor: "#000000", 
              color: "#FFFFFF", 
            },
          });
          localStorage.setItem("toastShown", "true");
        }
      }
    }
  }, [router]);
  
  

 

  return (

    <>
    <Navbar/>
    <div className="flex flex-col justify-center items-center h-screen">
      {username ? (
        <>
        
          <h1 className="text-4xl font-bold">Welcome, {username}!</h1>
         
        </>
      ) : (
        <h1 className="text-4xl font-bold">Loading...</h1>
      )}
    </div>

    
    </>  );
}
