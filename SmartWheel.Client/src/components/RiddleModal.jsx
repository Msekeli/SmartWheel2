import { useState } from "react";
import quiz from "../data/quiz.json";

function RiddleModal({ onSubmit, onClose, isLoading }) {
  const riddles = quiz;

  const [currentQuestion, setCurrentQuestion] = useState(0);
  const [answers, setAnswers] = useState(Array(riddles.length).fill(null));

  const handleSelect = (option) => {
    const updated = [...answers];
    updated[currentQuestion] = option.value;
    setAnswers(updated);
  };

  const handleNext = () => {
    if (answers[currentQuestion] === null) {
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
      <div className="modal-card riddle-card">
        <h2 className="modal-title">
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
                answers[currentQuestion] === option.value ? "selected" : ""
              }`}
              onClick={() => handleSelect(option)}
            >
              {option.label}
            </button>
          ))}
        </div>

        <button
          className="next-button"
          onClick={handleNext}
          disabled={isLoading || answers[currentQuestion] === null}
        >
          {currentQuestion === riddles.length - 1
            ? isLoading
              ? "Submitting..."
              : "Submit Answers"
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
