import Sidebar from "./Sidebar";
import Topbar from "./Topbar";

export default function Layout({ children, name }) {
  return (
    <div className="h-screen flex bg-base">
      <Sidebar />

      <div className="flex-1 flex flex-col overflow-hidden">
        <Topbar name={name}/>

        <main className="flex-1 p-8 bg-base overflow-y-auto scrollbar">
          {children}
        </main>
      </div>
    </div>
  );
}
