import { LogOut } from "lucide-react";

export default function Topbar() {
  return (
    <div className="
      h-16 
      bg-primary 
      border-slate-600 
      flex 
      items-center 
      justify-between 
      px-8"
    >
      <h1 className="text-2xl text-white font-medium">
        Welcome, Gustavo
      </h1>

      <button className="text-slate-300 hover:text-white transition">
        <LogOut />
      </button>
    </div>
  );
}