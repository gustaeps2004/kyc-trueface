import Layout from "../../components/base/Layout";
import { useState, useEffect } from "react";
import { GetTokenData } from "../../utils/functions/GetTokenData";

export function Dashboard() {
  const [loggedName, setloggedName] = useState("")

  const cards = [
    { title: "Consulted in the last week", value: 34 },
    { title: "Reproved in the last week", value: 15 },
    { title: "Approved in the last week", value: 19 },
    { title: "Pending manual action", value: 10 },
    { title: "Approved manually in the last month", value: 15 },
    { title: "Reproved manually in the last month", value: 3 },
  ];

  useEffect(() => {
    const tokenData = GetTokenData()
    setloggedName(tokenData?.user_name?.split(' ')[0])
  }, []);

  return (
    <Layout name={`Welcome, ${loggedName}`}>
      <div className="grid grid-cols-3 gap-10">
        {cards.map((card, index) => (
          <div
            key={index}
            className="
              bg-secondary
              text-white 
              p-10 
              rounded-xl 
              text-center 
              shadow-lg"
          >
            <p className="text-slate-400 mb-6">{card.title}</p>
            <h2 className="text-3xl font-semibold">{card.value}</h2>
          </div>
        ))}
      </div>
    </Layout>
  );
}