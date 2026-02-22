function Layout({ children }) {
  return (
    <div className="min-h-screen flex flex-col items-center">
      <header className="w-full py-6 text-center">
        <h1 className="text-4xl font-bold bg-linear-to-r from-purple-400 via-blue-400 to-yellow-400 bg-clip-text text-transparent">
          SmartWheel
        </h1>
      </header>

      <main className="w-full max-w-4xl px-4">{children}</main>
    </div>
  );
}

export default Layout;
