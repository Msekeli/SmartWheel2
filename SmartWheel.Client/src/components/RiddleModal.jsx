import { useState } from "react";

function RiddleModal({ onSubmit, onClose, isLoading }) {
  const riddles = [
    {
      question: "I speak without a mouth and hear without ears. What am I?",
      options: ["Echo", "Wind", "Shadow", "Whistle"],
    },
    {
      question: "What has keys but can't open locks?",
      options: ["Keyboard", "Piano", "Treasure chest", "Car"],
    },
    {
      question: "What has hands but cannot clap?",
      options: ["Clock", "Robot", "Statue", "Tree"],
    },
    {
      question: "What gets wetter the more it dries?",
      options: ["Towel", "Sponge", "Rain", "Soap"],
    },
    {
      question: "What has a neck but no head?",
      options: ["Bottle", "Shirt", "Guitar", "River"],
    },
  ];

  const [currentQuestion, setCurrentQuestion] = useState(0);
  const [answers, setAnswers] = useState(Array(riddles.length).fill(null));

  const handleSelect = (option) => {
    const updated = [...answers];
    updated[currentQuestion] = option;
    setAnswers(updated);
  };

  const handleNext = () => {
    if (answers[currentQuestion] === null) {
      alert("Please select an answer");
      return;
    }

    if (currentQuestion < riddles.length - 1) {
      setCurrentQuestion(currentQuestion + 1);
    } else {
      onSubmit(answers);
    }
  };

  const riddle = riddles[currentQuestion];

  return (
    <div className="modal-overlay">
      <div className="modal-card">
        <h2>
          Riddle {currentQuestion + 1} / {riddles.length}
        </h2>

        <div className="progress-bar">
          <div
            className="progress-fill"
            style={{
              width: `${((currentQuestion + 1) / riddles.length) * 100}%`,
            }}
          />
        </div>

        <p className="question-text">{riddle.question}</p>

        <div className="options-grid">
          {riddle.options.map((option, index) => (
            <button
              key={index}
              type="button"
              className={`option-button ${
                answers[currentQuestion] === option ? "selected" : ""
              }`}
              onClick={() => handleSelect(option)}
            >
              {option}
            </button>
          ))}
        </div>

        <button
          className="next-button"
          onClick={handleNext}
          disabled={isLoading}
        >
          {currentQuestion === riddles.length - 1
            ? isLoading
              ? "Submitting..."
              : "Submit"
            : "Next"}
        </button>

        <button className="cancel-button" onClick={onClose}>
          Cancel
        </button>
      </div>
    </div>
  );
}

export default RiddleModal;
