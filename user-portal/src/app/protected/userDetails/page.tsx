"use client";

import { useState, useEffect } from "react";
import { useRouter, useSearchParams } from "next/navigation";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { toast } from "sonner";
import { Toaster } from "@/components/ui/sonner";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";

export default function UpdatePage() {
    const router = useRouter();
    const searchParams = useSearchParams();

    const [firstname, setFirstname] = useState("");
    const [lastname, setLastname] = useState("");
    const [error, setError] = useState("");
    const token = localStorage.getItem("token");
    const [newPassword, setNewPassword] = useState("");
    const [oldPassword, setOldPassword] = useState("");



    type PasswordRequest = {
        oldPassword: string,
        newPassword: string
    };



    useEffect(() => {
        const queryFirstname = searchParams.get("firstname") || "";
        const queryLastname = searchParams.get("lastname") || "";
        setFirstname(queryFirstname);
        setLastname(queryLastname);
    }, [searchParams]);

    if (!token) {
        alert("Unauthorized. Please log in.");
        router.push("/auth/login");
        return null;
    }

    const decodedToken = jwtDecode(token);
    const email = decodedToken?.["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];



    const handleUpdate = async () => {
        if (!firstname.trim() || !lastname.trim()) {
            setError("Firstname and Lastname should not be empty.");
            return;
        }

        setError("");

        try {
            await axios.post(
                `https://localhost:7261/api/user/updateUser/${email}`,
                { firstname, lastname },
                { headers: { Authorization: `Bearer ${token}` } }
            );

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

    const handleChangePassword = async () => {
        if (!newPassword || !oldPassword) {
            setError("All fields are required.");
            return;
        }
        setError("");




        const passwordRequestObj: PasswordRequest = {
            oldPassword,
            newPassword,
        };


        console.log(passwordRequestObj);

        try {
            await axios.post(
                `https://localhost:7261/api/user/updatePassword`,
                passwordRequestObj,
                { headers: { Authorization: `Bearer ${token}` } }
            );
            toast("Password Updated", {
                description: "Password changed successfully",
                style: {
                    backgroundColor: "#000000",
                    color: "#FFFFFF",
                },
            });
        } catch (error: any) {
            console.log(error.response.data.message);


            toast("Failed ", {
                description: `${error.response.data.message}`,
                style: {
                    backgroundColor: "#000000",
                    color: "#FFFFFF",
                },
            });
        }
    };

    const handleBack = () => {
        router.push("/protected/home");
    };

    return (
        <div className="flex flex-col items-center justify-center min-h-screen p-4">
            <h1 className="text-2xl font-bold mb-6">Update Profile</h1>
            {error && <p className="text-red-500 mb-4">{error}</p>}
            <Toaster />

            <Tabs defaultValue="account" className="w-[400px] p-6 rounded-2xl shadow-2xl">
                <TabsList>
                    <TabsTrigger value="account">Account</TabsTrigger>
                    <TabsTrigger value="password">Password</TabsTrigger>
                </TabsList>


                <TabsContent value="account">
                    <label className="mb-2 block">Firstname:</label>
                    <input
                        type="text"
                        value={firstname}
                        onChange={(e) => setFirstname(e.target.value)}
                        className="p-2 border border-gray-300 rounded mb-4 w-full"
                        minLength={2}
                        maxLength={30}
                        required
                    />

                    <label className="mb-2 block">Lastname:</label>
                    <input
                        type="text"
                        value={lastname}
                        onChange={(e) => setLastname(e.target.value)}
                        className="p-2 border border-gray-300 rounded mb-4 w-full"
                        minLength={2}
                        maxLength={30}
                        required
                    />

                    <div className="flex justify-between">

                        <button onClick={handleUpdate} className="mt-4 bg-blue-500 text-white px-6 py-2 rounded hover:bg-blue-600">
                            Update Profile
                        </button>

                        <button onClick={handleBack} className="mt-4 bg-gray-500 text-white px-6 py-2 rounded hover:bg-gray-600">
                            Back
                        </button>


                    </div>

                </TabsContent>
                <TabsContent value="password">
                    <label className="mb-2 block">Old Password:</label>
                    <input
                        type="password"
                        value={oldPassword}
                        onChange={(e) => setOldPassword(e.target.value)}
                        className="p-2 border border-gray-300 rounded mb-4 w-full"
                        minLength={8}
                        required
                    />

                    <label className="mb-2 block">New Password:</label>
                    <input
                        type="password"
                        value={newPassword}
                        onChange={(e) => setNewPassword(e.target.value)}
                        className="p-2 border border-gray-300 rounded mb-4 w-full"
                        minLength={8}
                        required
                    />

                    <div className="flex justify-between">
                        <button onClick={handleChangePassword} className="mt-4 bg-green-500 text-white px-6 py-2 rounded hover:bg-green-600">
                            Submit
                        </button>
                        <button onClick={handleBack} className="mt-4 bg-gray-500 text-white px-6 py-2 rounded hover:bg-gray-600">
                            Back
                        </button>
                    </div>


                </TabsContent>
            </Tabs>

        </div>
    );
}
