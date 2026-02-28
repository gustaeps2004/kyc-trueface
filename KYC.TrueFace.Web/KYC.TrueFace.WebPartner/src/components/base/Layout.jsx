import Sidebar from "./Sidebar";
import Topbar from "./Topbar";

export default function Layout({ children, name }) {
  return (
    <div className="h-screen flex bg-slate-100">
      <Sidebar />

      <div className="flex-1 flex flex-col">
        <Topbar name={name}/>

        <main className="flex-1 p-10 bg-primary">
          {children}
        </main>
      </div>
    </div>
  );
}