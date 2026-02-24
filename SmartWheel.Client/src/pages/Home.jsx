import { useState } from "react";
import RouletteWheel from "../components/Wheel";
import EmailModal from "../components/EmailModal";
import { resolveIdentity } from "../services/smartWheelApi";

function Home() {
  const [userId, setUserId] = useState(null);
  const [balance, setBalance] = useState(0);
  const [canSpin, setCanSpin] = useState(false);
  const [showEmailModal, setShowEmailModal] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const handleSpinClick = () => {
    if (!userId) {
      setShowEmailModal(true);
      return;
    }

    // For now just log
    console.log("User ready to continue to riddles...");
  };

  const handleEmailSubmit = async (email) => {
    try {
      setIsLoading(true);

      const result = await resolveIdentity(email);

      setUserId(result.userId);
      setBalance(result.balance);
      setCanSpin(result.canSpin);

      setShowEmailModal(false);
    } catch (error) {
      console.error("Identity error:", error);
      alert("Failed to resolve user.");
    } finally {
      setIsLoading(false);
    }
  };
  <div style={{ marginTop: "20px", textAlign: "center" }}>
    {userId && (
      <>
        <p>Balance: R{balance}</p>
        <p>Can Spin: {canSpin ? "Yes" : "No"}</p>
      </>
    )}
  </div>;

  return (
    <>
      <RouletteWheel onSpinClick={handleSpinClick} />

      {showEmailModal && (
        <EmailModal
          onSubmit={handleEmailSubmit}
          onClose={() => setShowEmailModal(false)}
          isLoading={isLoading}
        />
      )}
    </>
  );
}

export default Home;
