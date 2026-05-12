import Layout from "../../components/base/Layout";

export function Dashboard() {
  const cards = [
    { title: "Consulted in the last week", value: 34 },
    { title: "Reproved in the last week", value: 15 },
    { title: "Approved in the last week", value: 19 },
    { title: "Pending manual action", value: 10 },
    { title: "Approved manually in the last month", value: 15 },
    { title: "Reproved manually in the last month", value: 3 },
  ];

  return (
    <Layout name="Welcome, Gustavo">
      <div className="grid grid-cols-3 gap-10">
        {cards.map((card, index) => (
          <div
            key={index}
            className="
              bg-secondary 
              p-10 
              text-title
              rounded-2xl 
              text-center 
              shadow-xl 
              border 
              border-border-ui
              transition-transform 
              hover:scale-105 
              duration-300"
          >
            <p className="text-text-muted text-sm uppercase tracking-wider mb-4">{card.title}</p>
            <h2 className="text-title text-5xl font-bold tracking-tight">{card.value}</h2>
          </div>
        ))}
      </div>
    </Layout>
  );
}